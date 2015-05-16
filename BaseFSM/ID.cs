using System;

namespace BaseFSM
{
	public abstract class ID<T>
	{
		public T id
		{
			get;
			protected set;
		}

		public ID(T id)
		{
			this.id = id;
		}

		public override bool Equals(object obj)
		{
			return id.Equals(obj);
		}

		public override int GetHashCode()
		{
			return id.GetHashCode();
		}
	}
}
