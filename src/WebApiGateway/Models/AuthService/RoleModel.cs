namespace WebApiGateway.Models.AuthService
{
    /// <summary>
    /// Role entity
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleModel"/> class.
        /// </summary>
        public RoleModel()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string? Name { get; set; }
    }
}