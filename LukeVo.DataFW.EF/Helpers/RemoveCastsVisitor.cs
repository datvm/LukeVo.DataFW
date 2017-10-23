using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LukeVo.DataFW.EF.Helpers
{
    internal class RemoveCastsVisitor : ExpressionVisitor
    {
        private static readonly ExpressionVisitor Default = new RemoveCastsVisitor();

        private RemoveCastsVisitor()
        {
        }

        public new static Expression Visit(Expression node)
        {
            return Default.Visit(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert
                && node.Type.GetTypeInfo().IsAssignableFrom(node.Operand.Type))
            {
                return base.Visit(node.Operand);
            }
            return base.VisitUnary(node);
        }
    }

}