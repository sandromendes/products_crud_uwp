using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCRUD.Util.Messages.Product
{
    public static class ProductMessages
    {
        public static readonly string PRODUCT_CREATED_TITLE = "Produto Adicionado";
        public static readonly string PRODUCT_CREATED_CONTENT = "Produto adicionado com sucesso!";        
        public static readonly string PRODUCT_REMOVED_TITLE = "Produto Removido";
        public static readonly string PRODUCT_REMOVED_CONTENT = "Produto adicionado com sucesso!";
        public static readonly string PRODUCT_UPDATED_TITLE = "Produto Atualizado";
        public static readonly string PRODUCT_UPDATED_CONTENT = $"Produto \"{0}\" atualizado com sucesso!";
    }
}
