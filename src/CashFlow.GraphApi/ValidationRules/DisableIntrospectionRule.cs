using System.Threading.Tasks;
using GraphQL.Language.AST;
using GraphQL.Validation;

namespace CashFlow.GraphApi.ValidationRules
{
    internal sealed class DisableIntrospectionRule : IValidationRule
    {
        public Task<INodeVisitor> ValidateAsync(ValidationContext context)
            => Task.FromResult(Validate(context));

        private static INodeVisitor Validate(ValidationContext context)
        {
            return new EnterLeaveListener(_ =>
            {
                _.Match<Field>(field =>
                {
                    if (field.Name.Equals("__schema") || field.Name.Equals("__type"))
                    {
                        // Error code based on: https://github.com/graphql-dotnet/graphql-dotnet/tree/974655cb3dd9b1e85cfd33095b05bf5beb2978fb/src/GraphQL/Validation/Rules
                        context.ReportError(new ValidationError(context.OriginalQuery, "5.9.1.1", "GraphQL introspection is not allowed, because the query contained __schema or __type", field));
                    }
                });
            });
        }
    }
}
