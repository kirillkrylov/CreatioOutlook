using CreatioDataService.DTO;
using System.Collections.Generic;
using Terrasoft.Core.Entities;
using Terrasoft.Nui.ServiceModel.DataContract;

namespace CreatioDataService
{
    internal static class InsertQuerryBuilder
    {
        internal static InsertQuery GetLeadInsertQuery(Lead lead)
        {
            InsertQuery insert = new InsertQuery
            {
                RootSchemaName = "Lead",
                ColumnValues = new ColumnValues()
                {
                    Items = new Dictionary<string, ColumnExpression>()
                    {
                        {
                           nameof(lead.Id), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Guid,
                                    Value = lead.Id
                                }
                            }
                        },
                        {
                            nameof(lead.Contact), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Text,
                                    Value = lead.Contact
                                }
                            }
                        },
                        {
                            nameof(lead.Account), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Text,
                                    Value = lead.Account
                                }
                            }
                        },
                        {
                            nameof(lead.Email), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Text,
                                    Value = lead.Email
                                }
                            }
                        },
                        {
                            nameof(lead.Owner), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Guid,
                                    Value = lead.Owner
                                }
                            }
                        },
                        {
                            nameof(lead.Notes), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Text,
                                    Value = lead.Notes
                                }
                            }
                        },
                        {
                            nameof(lead.Commentary), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Text,
                                    Value = lead.Commentary
                                }
                            }
                        },
                        {
                            nameof(lead.LeadType), new ColumnExpression
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    DataValueType = DataValueType.Guid,
                                    Value = lead.LeadType
                                }
                            }
                        }
                    }
                       
                },
            };
            return insert;

        }
    }
}
