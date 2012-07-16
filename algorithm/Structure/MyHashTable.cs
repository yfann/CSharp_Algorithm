using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class Hashtable
{
    private struct bucket
    {
        public Object key; //键
        public Object val; //值
        public int hash_coll; //哈希码
    }

    private bucket[] buckets; //存储哈希表数据的数组（数据桶）
    private int count; //元素个数
    private int loadsize; //当前允许存储的元素个数
    private float loadFactor; //填充因子

    //默认构造方法
    public Hashtable() : this(0, 1.0f) { }

    //指定容量的构造方法
    public Hashtable(int capacity, float loadFactor)
    {
        if (!(loadFactor >= 0.1f && loadFactor <= 1.0f))
            throw new ArgumentOutOfRangeException(
                "填充因子必须在0.1～1之间");
        this.loadFactor = loadFactor > 0.72f ? 0.72f : loadFactor;
        //根据容量计算表长
        double rawsize = capacity / this.loadFactor;
        int hashsize = (rawsize > 11) ? //表长为大于11的素数
             GetPrime((int)rawsize) : 11;
        buckets = new bucket[hashsize]; //初始化容器
        loadsize = (int)(this.loadFactor * hashsize);
    }

    public virtual void Add(Object key, Object value) //添加
    {
        Insert(key, value, true);
    }

    //哈希码初始化
    private uint InitHash(Object key, int hashsize,
        out uint seed, out uint incr)
    {
        uint hashcode = (uint)GetHash(key) & 0x7FFFFFFF; //取绝对值
        seed = (uint)hashcode; //h1
        incr = (uint)(1 + (((seed >> 5) + 1) % ((uint)hashsize - 1)));//h2
        return hashcode; //返回哈希码
    }

    public virtual Object this[Object key] //索引器
    {
        get
        {
            uint seed; //h1
            uint incr; //h2
            uint hashcode = InitHash(key, buckets.Length,
                out seed, out incr);
            int ntry = 0; //用于表示h(key,i)中的i值
            bucket b;
            int bn = (int)(seed % (uint)buckets.Length); //h(key,0)
            do
            {
                b = buckets[bn];
                if (b.key == null) //b为无冲突空位时
                {  //找不到相应的键，返回空
                    return null;
                }
                if (((b.hash_coll & 0x7FFFFFFF) == hashcode) &&
                    KeyEquals(b.key, key))
                {   //查找成功
                    return b.val;
                }
                bn = (int)(((long)bn + incr) %
                    (uint)buckets.Length); //h(key+i)
            } while (b.hash_coll < 0 && ++ntry < buckets.Length);
            return null;
        }
        set
        {
            Insert(key, value, false);
        }
    }

    private void expand() //扩容
    {   //使新的容量为旧容量的近似两倍
        int rawsize = GetPrime(buckets.Length * 2);
        rehash(rawsize);
    }

    private void rehash(int newsize) //按新容量扩容
    {
        bucket[] newBuckets = new bucket[newsize];
        for (int nb = 0; nb < buckets.Length; nb++)
        {
            bucket oldb = buckets[nb];
            if ((oldb.key != null) && (oldb.key != buckets))
            {
                putEntry(newBuckets, oldb.key, oldb.val,
                    oldb.hash_coll & 0x7FFFFFFF);
            }
        }
        buckets = newBuckets;
        loadsize = (int)(loadFactor * newsize);
        return;
    }

    //在新数组内添加旧数组的一个元素
    private void putEntry(bucket[] newBuckets, Object key,
        Object nvalue, int hashcode)
    {
        uint seed = (uint)hashcode; //h1
        uint incr = (uint)(1 + (((seed >> 5) + 1) %
            ((uint)newBuckets.Length - 1))); //h2
        int bn = (int)(seed % (uint)newBuckets.Length);//哈希地址
        do
        {   //当前位置为有冲突空位或无冲突空位时都可添加新元素
            if ((newBuckets[bn].key == null) ||
                (newBuckets[bn].key == buckets))
            {   //赋值
                newBuckets[bn].val = nvalue;
                newBuckets[bn].key = key;
                newBuckets[bn].hash_coll |= hashcode;
                return;
            }
            //当前位置已存在其他元素时
            if (newBuckets[bn].hash_coll >= 0)
            {   //置hash_coll的高位为1
                newBuckets[bn].hash_coll |=
                    unchecked((int)0x80000000);
            }
            //二度哈希h1(key)+h2(key)
            bn = (int)(((long)bn + incr) % (uint)newBuckets.Length);
        } while (true);
    }

    protected virtual int GetHash(Object key)
    {   //获取哈希码
        return key.GetHashCode();
    }

    protected virtual bool KeyEquals(Object item, Object key)
    {   //用于判断两key是否相等
        return item == null ? false : item.Equals(key);
    }

    //当add为true时用作添加元素，当add为false时用作修改元素值
    private void Insert(Object key, Object nvalue, bool add)
    {   //如果超过允许存放元素个数的上限则扩容
        if (count >= loadsize)
        {
            expand();
        }
        uint seed; //h1
        uint incr; //h2
        uint hashcode = InitHash(key, buckets.Length, out seed, out incr);
        int ntry = 0; //用于表示h(key,i)中的i值
        int emptySlotNumber = -1; //用于记录空位
        int bn = (int)(seed % (uint)buckets.Length); //索引号
        do
        {   //如果是有冲突空位，需继续向后查找以确定是否存在相同的键
            if (emptySlotNumber == -1 && (buckets[bn].key == buckets) &&
                (buckets[bn].hash_coll < 0))
            {
                emptySlotNumber = bn;
            }
            if (buckets[bn].key == null) //确定没有重复键才添加
            {
                if (emptySlotNumber != -1) //使用之前的空位
                    bn = emptySlotNumber;
                buckets[bn].val = nvalue;
                buckets[bn].key = key;
                buckets[bn].hash_coll |= (int)hashcode;
                count++;
                return;
            }
            //找到重复键
            if (((buckets[bn].hash_coll & 0x7FFFFFFF) == hashcode) &&
                KeyEquals(buckets[bn].key, key))
            {   //如果处于添加元素状态，则由于出现重复键而报错
                if (add)
                {
                    throw new ArgumentException("添加了重复的键值！");
                }
                buckets[bn].val = nvalue; //修改批定键的元素
                return;
            }
            //存在冲突则置hash_coll的最高位为1
            if (emptySlotNumber == -1)
            {
                if (buckets[bn].hash_coll >= 0)
                {
                    buckets[bn].hash_coll |= unchecked((int)0x80000000);
                }
            }
            bn = (int)(((long)bn + incr) % (uint)buckets.Length);//二度哈希
        } while (++ntry < buckets.Length);
        throw new InvalidOperationException("添加失败！");
    }

    public virtual void Remove(Object key) //移除一个元素
    {
        uint seed; //h1
        uint incr; //h2
        uint hashcode = InitHash(key, buckets.Length, out seed, out incr);
        int ntry = 0; //h(key,i)中的i
        bucket b;
        int bn = (int)(seed % (uint)buckets.Length); //哈希地址
        do
        {
            b = buckets[bn];
            if (((b.hash_coll & 0x7FFFFFFF) == hashcode) &&
                KeyEquals(b.key, key)) //如果找到相应的键值
            {   //保留最高位，其余清0
                buckets[bn].hash_coll &= unchecked((int)0x80000000);
                if (buckets[bn].hash_coll != 0) //如果原来存在冲突
                {   //使key指向buckets
                    buckets[bn].key = buckets;
                }
                else //原来不存在冲突
                {   //置key为空
                    buckets[bn].key = null;
                }
                buckets[bn].val = null;  //释放相应的“值”。
                count--;
                return;
            } //二度哈希
            bn = (int)(((long)bn + incr) % (uint)buckets.Length);
        } while (b.hash_coll < 0 && ++ntry < buckets.Length);
    }

    public override string ToString()
    {
        string s = string.Empty;
        for (int i = 0; i < buckets.Length; i++)
        {
            if (buckets[i].key != null && buckets[i].key != buckets)
            {   //不为空位时打印索引、键、值、hash_coll
                s += string.Format("{0,-5}{1,-8}{2,-8}{3,-8}\r\n",
                    i.ToString(), buckets[i].key.ToString(),
                    buckets[i].val.ToString(),
                    buckets[i].hash_coll.ToString());
            }
            else
            {   //是空位时则打印索引和hash_coll
                s += string.Format("{0,-21}{1,-8}\r\n", i.ToString(),
                    buckets[i].hash_coll.ToString());
            }
        }
        return s;
    }

    public virtual int Count //属性
    {   //获取元素个数
        get { return count; }
    }

    public int GetPrime(object val)
    {
        string type = "System.Collections.HashHelpers,mscorlib,Version=2.0.0.0,Culture=neutral,PublicKeyToken=b77a5c561934e089";
        Type hashHelpes = Type.GetType(type);
        object result = hashHelpes.InvokeMember("GetPrime", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Static, null, null, new object[] { val });
        int res = Convert.ToInt32(result);
        return res;
    }
}