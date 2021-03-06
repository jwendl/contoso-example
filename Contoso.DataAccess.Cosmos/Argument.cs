﻿using System;
using System.Linq.Expressions;

namespace Contoso.DataAccess.Cosmos
{
    public class Argument
    {
        public static void IsNotNull<TResult>(Expression<Func<TResult>> expression)
        {
            if (expression.Compile().Invoke() == null)
            {
                throw new ArgumentNullException(nameof(TResult));
            }
        }
    }
}
