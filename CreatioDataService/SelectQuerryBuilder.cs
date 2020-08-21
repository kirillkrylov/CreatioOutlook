using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrasoft.Common;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.GlobalSearch.Cancellation;
using Terrasoft.Nui.ServiceModel.DataContract;
using FilterType = Terrasoft.Nui.ServiceModel.DataContract.FilterType;

namespace CreatioDataService
{
    internal static class SelectQuerryBuilder
    {
        internal static SelectQuery GetCurrentContact(string userName)
        {
            SelectQuery select = new SelectQuery()
            {
                RootSchemaName = "SysAdminUnit",
                UseLocalization = false,
                Columns = new SelectQueryColumns()
                {
                    Items = new Dictionary<string, SelectQueryColumn>()
                    {
                        {
                            "Id", new SelectQueryColumn()
                            {
                                Expression = new ColumnExpression()
                                {
                                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                                    ColumnPath = "Id"
                                }
                            }
                        },
                        {
                            "Name", new SelectQueryColumn()
                            {
                                Expression = new ColumnExpression()
                                {
                                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                                    ColumnPath = "Contact.Name"
                                }
                            }
                        },
                        {
                            "Email", new SelectQueryColumn()
                            {
                                Expression = new ColumnExpression()
                                {
                                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                                    ColumnPath = "Contact.Email"
                                }
                            }
                        },
                        {
                            "Contact.Id", new SelectQueryColumn()
                            {
                                Expression = new ColumnExpression()
                                {
                                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                                    ColumnPath = "Contact.Id"
                                }
                            }
                        },

                    }
                },
                Filters = new Filters
                {
                    LogicalOperation = LogicalOperationStrict.Or,
                    FilterType = FilterType.FilterGroup,
                    Items = new Dictionary<string, Filter>
                    {
                        {
                            "FilterByUserName", new Filter()
                            {
                                FilterType = FilterType.CompareFilter,
                                ComparisonType = FilterComparisonType.Equal,
                                LeftExpression = new BaseExpression
                                {
                                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                                    ColumnPath = "Name"
                                },
                                RightExpression = new BaseExpression
                                {
                                    ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                    Parameter = new Parameter
                                    {
                                        DataValueType = DataValueType.Text,
                                        Value = userName
                                    }
                                }
                                
                            }
                        }
                    }
                }
            };
            return select;
        }

    }
}
