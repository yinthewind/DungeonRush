public class MonitoredValue<T>
{
	public delegate void OnChangeEventHandler(T oldVal, T newVal);
	public OnChangeEventHandler OnChange;

	private T val;
	public T Val
	{
		get
		{
			return val;
		}
		set
		{
			if (OnChange != null)
			{
				OnChange(val, value);
			}
			val = value;
		}
	}
}
