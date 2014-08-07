namespace SocialShopper.Core.Entities.Interface
{
	public interface IHaveStringValue : IHaveValue<string>
    {
    }

	public interface IHaveIntValue : IHaveValue<int>
	{
	}

	public interface IHaveFloatValue : IHaveValue<float>
	{
	}

	public interface IHaveValue<T>
	{
		T Value { get; set; }
	}
}