using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace ${SolutionName}.Base.Mvvm.Behavior
{

	public static class EventHandlerGenerator
	{

		public static Delegate CreateDelegate(Type eventHandlerType, MethodInfo methodToInvoke, object methodInvoker)
		{
			var eventHandlerInfo = eventHandlerType.GetMethod("Invoke");
			Type returnType = eventHandlerInfo.ReturnParameter.ParameterType;

			if (returnType != typeof(void))
				throw new ApplicationException("Delegate has a return type. This only supprts event handlers that are void");

			ParameterInfo[] delegateParameters = eventHandlerInfo.GetParameters();
			Type[] hookupParameters = new Type[delegateParameters.Length + 1];
			hookupParameters[0] = methodInvoker.GetType();

			for (int i = 0; i < delegateParameters.Length; i++)
				hookupParameters[i + 1] = delegateParameters[i].ParameterType;

			DynamicMethod handler = new DynamicMethod(
				"", null,
				hookupParameters, typeof(EventHandlerGenerator)
			);

			ILGenerator eventIL = handler.GetILGenerator();
			LocalBuilder local = eventIL.DeclareLocal(typeof(object[]));

			eventIL.Emit(OpCodes.Ldc_I4, delegateParameters.Length + 1);
			eventIL.Emit(OpCodes.Newarr, typeof(object));
			eventIL.Emit(OpCodes.Stloc, local);

			for (int i = 1; i < delegateParameters.Length + 1; i++)
			{
				eventIL.Emit(OpCodes.Ldloc, local);
				eventIL.Emit(OpCodes.Ldc_I4, i);
				eventIL.Emit(OpCodes.Ldarg, i);
				eventIL.Emit(OpCodes.Stelem_Ref);
			}

			eventIL.Emit(OpCodes.Ldloc, local);
			eventIL.Emit(OpCodes.Ldarg_0);
			eventIL.EmitCall(OpCodes.Call, methodToInvoke, null);
			eventIL.Emit(OpCodes.Pop);
			eventIL.Emit(OpCodes.Ret);

			return handler.CreateDelegate(eventHandlerType, methodInvoker);
		}

	}

}