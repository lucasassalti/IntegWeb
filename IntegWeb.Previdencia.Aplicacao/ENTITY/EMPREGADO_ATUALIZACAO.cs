using System;

namespace IntegWeb.Previdencia.Aplicacao.ENTITY
{
    public partial class EMPREGADO_ATUALIZACAO
    {
        public long NUM_CPF { get; set; }
        public DateTime DAT_NASCM { get; set; }
        public string COD_DDD { get; set; }
        public Nullable<int> NUM_TELEF { get; set; }
        public string COD_DDDCEL { get; set; }
        public Nullable<int> NUM_CELUL { get; set; }
        public string COD_EMAIL { get; set; }
        public short TIP_EMPREGADO { get; set; }
        public Guid GUI_EMAIL { get; set; }
        public string CHR_EMAIL_CONFIRM { get; set; }
        public Nullable<DateTime> DAT_ATUALIZACAO { get; set; }
    }
}
