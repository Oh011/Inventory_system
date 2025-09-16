namespace InventorySystem.Application.Features.Users.Specifications
{
    //internal class UserSpecifications : IProjectionSpecifications<IApplicationUser, UserSummaryDto>, ISpecification<IApplicationUser>, IIncludeSpecification<IApplicationUser>
    //{
    //    public Expression<Func<IApplicationUser, bool>> Criteria { get; private set; }

    //    public List<Expression<Func<IApplicationUser, object>>> IncludeExpressions { get; private set; }

    //    public Expression<Func<IApplicationUser, UserSummaryDto>> Projection { get; private set; }

    //    public Expression<Func<UserSummaryDto, object>>? ResultOrderBy { get; private set; }

    //    public Expression<Func<UserSummaryDto, object>>? ResultOrderByDescending { get; private set; }

    //    public void AddProjection(Expression<Func<IApplicationUser, UserSummaryDto>> projection)
    //    {

    //        Projection = projection;
    //    }

    //    public void SetResultOrderBy(Expression<Func<UserSummaryDto, object>> expression)
    //    {

    //        ResultOrderBy = expression;
    //    }

    //    public void SetResultOrderByDescending(Expression<Func<UserSummaryDto, object>> expression)
    //    {
    //        ResultOrderByDescending = expression;
    //    }


    //    public UserSpecifications(GetUsersQuery query)
    //    {

    //        this.Criteria = (u =>

    //        string.IsNullOrEmpty(query.UserName) || u.UserName.ToLower().Contains(query.UserName.ToLower())

    //        && string.IsNullOrEmpty(query.email) || u.Email.ToLower().Contains(query.email.ToLower())




    //        );



    //    }
    //}
}
