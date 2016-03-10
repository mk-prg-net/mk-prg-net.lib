using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ET = System.Linq.Expressions;
using Exp = System.Linq.Expressions.Expression;
using System.Diagnostics;

namespace mko.Algo.Test
{
    [TestClass]
    public class ExpressionTrees
    {
        [TestMethod]
        public void mko_algo_simple_expression_trees()
        {

            //Func<long, long, long> add = (a, b) => a + b;
            ET.Expression<Func<long, long, long>> binAdd = (x, y) => x + y;

            Assert.IsTrue(binAdd.NodeType == ET.ExpressionType.Lambda);

            var fakt = Exp.Parameter(typeof(long), "fakt");
            var a = Exp.Parameter(typeof(long), "a");
            var b = Exp.Parameter(typeof(long), "b");

            //ET.Expression<Func<long, long, long>> mulAdd = (a, b) => a * binAdd.(a, b);
            ET.BinaryExpression mulAdd = ET.Expression.Multiply(
                fakt,
                Exp.Add(a, b));
            //Exp.Call(binAdd, add.Method, Exp.Parameter(typeof(long), "a"), Exp.Parameter(typeof(long), "b")));


            var exec = ET.Expression.Lambda<Func<long, long, long, long>>(mulAdd,
                 fakt,
                 a,
                 b
                 ).Compile()(10, 2, 3);



            var c1 = Exp.Constant(2);
            var c2 = Exp.Constant(3);
            var mul2_3 = Exp.Multiply(c1, c2);
            var res = Exp.Lambda<Func<int>>(mul2_3, null).Compile()();


            var myVisit = new ToJson();

            myVisit.StartVisit(mulAdd);
            Debug.WriteLine(myVisit.ToString());

            myVisit.StartVisit(mul2_3);
            Debug.WriteLine(myVisit.ToString());




        }


        class ToJson : ET.ExpressionVisitor
        {

            System.Text.StringBuilder JsonStringBld = new System.Text.StringBuilder();

            public override string ToString()
            {
                return JsonStringBld.ToString();
            }            

            public Exp StartVisit(Exp node)
            {
                JsonStringBld.Clear();
                return base.Visit(node);
            }

            protected override Exp VisitConstant(ET.ConstantExpression node)
            {

                JsonStringBld.Append("(Const ");
                JsonStringBld.Append(node.Type.Name);
                JsonStringBld.Append(" ");
                JsonStringBld.Append(node.ToString());
                JsonStringBld.Append(")");
                return base.VisitConstant(node);
            }

            protected override Exp VisitParameter(ET.ParameterExpression node)
            {
                JsonStringBld.Append("(Param ");
                JsonStringBld.Append(node.Type.Name);
                JsonStringBld.Append(" ");
                JsonStringBld.Append(node.Name);                
                JsonStringBld.Append(")");
                return base.VisitParameter(node);
            }

            protected override Exp VisitBinary(ET.BinaryExpression node)
            {
                if (node.NodeType == ET.ExpressionType.Add)
                {
                    JsonStringBld.Append("{ADD ");

                    Visit(node.Left);

                    JsonStringBld.Append(" ");

                    Visit(node.Right);

                    JsonStringBld.Append("}");

                    return node;
                }
                else if (node.NodeType == ET.ExpressionType.Multiply)
                {
                    JsonStringBld.Append("{MUL ");

                    Visit(node.Left);

                    JsonStringBld.Append(" ");

                    Visit(node.Right);

                    JsonStringBld.Append("}");

                    return node;

                }

                else
                    return base.VisitBinary(node);
            }
        }
    }
}
