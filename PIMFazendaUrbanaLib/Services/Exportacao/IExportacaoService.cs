﻿namespace PIMFazendaUrbanaLib
{
    public interface IExportacaoService
    {
        byte[] Exportar(IEnumerable<object> dados, string formato);
    }
}