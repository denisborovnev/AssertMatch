﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EasyTests.Visitors
{
    public class MemberAccessTypeReader : ExpressionVisitor
    {
        private Type MemberType { get; set; }

        public static Type Read(Expression expression)
        {
            var visitor = new MemberAccessTypeReader();
            visitor.Visit(expression);
            return visitor.MemberType;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (MemberType != null)
            {
                return node;
            }
            
            if (node.Member is PropertyInfo property)
            {
                MemberType = property.PropertyType;
                return node;
            }

            if (node.Member is FieldInfo field)
            {
                MemberType = field.FieldType;
                return node;
            }

            throw new Exception($"Unsupported MemberType {node.Member.MemberType}, expression: {node}");
        }
    }
}