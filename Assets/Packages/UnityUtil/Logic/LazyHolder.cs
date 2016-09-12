public class LazyHolder<T>
{
    bool _hasValue;
    T _value;
    System.Func<T> getter;
    public T value
    {
        get
        {
            if (!_hasValue)
            {
                _hasValue = true;
                _value = getter();
            }
            return _value;
        }
    }

    public LazyHolder(System.Func<T> getter)
    {
        this.getter = getter;
    }

    public static implicit operator T(LazyHolder<T> self) { return self.value; }
}
