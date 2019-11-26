using MediatR;

using System.Data;

namespace Rems.Application.DB.Queries
{
    public class GetDataTableQuery : IRequest<DataTable>
    {
        public string TableName { get; set; }        
    }
}
