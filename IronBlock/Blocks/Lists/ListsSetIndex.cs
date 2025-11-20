using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace IronBlock.Blocks.Lists
{
  public class ListsSetIndex : IBlock
  {
    // Helper method to create list.Insert(index, value) invocation
    ExpressionSyntax CreateInsert(ExpressionSyntax list, ExpressionSyntax index, ExpressionSyntax value)
    {
      return InvocationExpression(
        MemberAccessExpression(
          SyntaxKind.SimpleMemberAccessExpression,
          list,
          IdentifierName("Insert")
        )
      )
      .WithArgumentList(
        ArgumentList(
          SeparatedList<ArgumentSyntax>(
            new SyntaxNodeOrToken[]
            {
              Argument(index),
              Token(SyntaxKind.CommaToken),
              Argument(value)
            }
          )
        )
      );
    }

    public override SyntaxNode Generate(Context context)
    {
      var listExpression = this.Values.Generate("LIST", context) as ExpressionSyntax;
      if (listExpression == null) throw new ApplicationException($"Unknown expression for list.");

      var toExpression = this.Values.Generate("TO", context) as ExpressionSyntax;
      if (toExpression == null) throw new ApplicationException($"Unknown expression for to.");

      ExpressionSyntax atExpression = null;
      if (this.Values.Any(x => x.Name == "AT"))
      {
        atExpression = this.Values.Generate("AT", context) as ExpressionSyntax;
      }

      var mode = this.Fields.Get("MODE");
      switch (mode)
      {
        case "SET":
          break;
        case "INSERT_AT":
        case "INSERT":
          break;
        default: throw new NotSupportedException($"unknown mode {mode}");
      }

      SyntaxNode listSet = null;

      var where = this.Fields.Get("WHERE");
      
      if (mode == "INSERT")
      {
        switch (where)
        {
          case "FROM_START":
            if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");
            listSet = CreateInsert(
              listExpression,
              BinaryExpression(
                SyntaxKind.SubtractExpression,
                atExpression,
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))
              ),
              toExpression
            );
            break;

          case "FROM_END":
            listSet = CreateInsert(
              listExpression,
              BinaryExpression(
                SyntaxKind.SubtractExpression,
                MemberAccessExpression(
                  SyntaxKind.SimpleMemberAccessExpression,
                  listExpression,
                  IdentifierName("Count")
                ),
                atExpression
              ),
              toExpression
            );
            break;

          case "FIRST":
            listSet = CreateInsert(
              listExpression,
              LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0)),
              toExpression
            );
            break;

          case "LAST":
            listSet = CreateInsert(
              listExpression,
              MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                listExpression,
                IdentifierName("Count")
              ),
              toExpression
            );
            break;

          case "RANDOM":
            listSet = CreateInsert(
              listExpression,
              InvocationExpression(
                MemberAccessExpression(
                  SyntaxKind.SimpleMemberAccessExpression,
                  ObjectCreationExpression(IdentifierName("Random"))
                    .WithArgumentList(ArgumentList()),
                  IdentifierName("Next")
                )
              )
              .WithArgumentList(
                ArgumentList(
                  SingletonSeparatedList(
                    Argument(
                      MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        listExpression,
                        IdentifierName("Count")
                      )
                    )
                  )
                )
              ),
              toExpression
            );
            break;

          default:
            throw new NotSupportedException($"unknown where {where}");
        }
      }
      else // SET mode
      {
        switch (where)
        {
          case "FROM_START":
            if (atExpression == null) throw new ApplicationException($"Unknown expression for at.");

            listSet = AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                ElementAccessExpression(
                    listExpression
                )
                .WithArgumentList(
                    BracketedArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    atExpression,
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(1)
                                    )
                                )
                            )
                        )
                    )
                ),
                toExpression
            );
            break;

          case "FROM_END":
            listSet = AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                ElementAccessExpression(
                    listExpression
                )
                .WithArgumentList(
                    BracketedArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        listExpression,
                                        IdentifierName("Count")
                                    ),
                                    atExpression
                                )
                            )
                        )
                    )
                ),
                toExpression
            );
            break;

          case "FIRST":
            listSet = AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                ElementAccessExpression(
                    listExpression
                )
                .WithArgumentList(
                    BracketedArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                LiteralExpression(
                                    SyntaxKind.NumericLiteralExpression,
                                    Literal(0)
                                )
                            )
                        )
                    )
                ),
                toExpression
            );
            break;

          case "LAST":
            listSet = AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                ElementAccessExpression(
                    listExpression
                )
                .WithArgumentList(
                    BracketedArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        listExpression,
                                        IdentifierName("Count")
                                    ),
                                    LiteralExpression(
                                        SyntaxKind.NumericLiteralExpression,
                                        Literal(1)
                                    )
                                )
                            )
                        )
                    )
                ),
                toExpression
            );
            break;

          case "RANDOM":
            listSet = AssignmentExpression(
              SyntaxKind.SimpleAssignmentExpression,
              ElementAccessExpression(listExpression)
                .WithArgumentList(
                  BracketedArgumentList(
                    SingletonSeparatedList(
                      Argument(
                        InvocationExpression(
                          MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            ObjectCreationExpression(IdentifierName("Random"))
                              .WithArgumentList(ArgumentList()),
                            IdentifierName("Next")
                          )
                        )
                        .WithArgumentList(
                          ArgumentList(
                            SingletonSeparatedList(
                              Argument(
                                MemberAccessExpression(
                                  SyntaxKind.SimpleMemberAccessExpression,
                                  listExpression,
                                  IdentifierName("Count")
                                )
                              )
                            )
                          )
                        )
                      )
                    )
                  )
                ),
              toExpression
            );
            break;

          default:
            throw new NotSupportedException($"unknown where {where}");
        }
      }

      return Statement(listSet, base.Generate(context), context);
    }
  }
}