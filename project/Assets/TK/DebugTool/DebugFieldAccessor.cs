using System.Collections;
using System.Reflection;

namespace TK.DebugTool
{

	public class DebugFieldAccessor<ValueType>
	{

		private object obj;
		private FieldInfo field;

		public ValueType Value
		{
			get { return (ValueType)field.GetValue(obj); }
			set { field.SetValue(obj, value); }
		}

		private DebugFieldAccessor()
		{
		}

		public static DebugFieldAccessor<ValueType> Bind(object obj, string fieldName)
		{

			DebugFieldAccessor<ValueType> accessor = new DebugFieldAccessor<ValueType> ();
			FieldInfo field = obj.GetType ().GetField (fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
			accessor.obj = obj;
			accessor.field = field;
			return accessor;

		}

	}

}
