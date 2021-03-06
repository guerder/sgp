using System;
using System.Collections.Generic;
using System.Linq;

namespace sgp.Models
{
  [Serializable]
  public class ControleEstoqueVenda
  {
    public List<Loja> Lojas { get; }
    public List<SetorEntrega> SetorEntregas { get; }

    public ControleEstoqueVenda()
    {
      this.Lojas = new List<Loja>();
      this.SetorEntregas = new List<SetorEntrega>();
    }

    public void AdicionarNovaEntrega(SetorEntrega entrega)
    {
      this.SetorEntregas.Add(entrega);
    }

    public void AdicionarLoja(Loja loja)
    {
      this.Lojas.Add(loja);
    }

    public bool ExisteLoja()
    {
      return Lojas.Count != 0;
    }

    public bool ExistemPedidos()
    {
      return Lojas.Any(Loja => Loja.Pedidos.Count != 0);
    }

    public bool ExistemEntregas()
    {
      return SetorEntregas.Count != 0;
    }

    public List<Loja> GetLojas()
    {
      return Lojas;
    }

    public int nextIdProduto()
    {
      int lastId = 0;
      try
      {
        lastId = ListarEstoques().Select(x => x.Produto.Codigo).Max();
      }
      catch { }
      return ++lastId;
    }

    public int nextIdPedido()
    {
      int lastId = 0;
      try
      {
        lastId = ListarPedidos().Select(x => x.Codigo).Max();
      }
      catch { }
      return ++lastId;
    }

    public List<Pedido> ListarPedidos()
    {
      List<Pedido> lista = new List<Pedido>();
      foreach (var loja in Lojas)
      {
        foreach (var pedido in loja.Pedidos)
        {
          lista.Add(pedido);
        }
      }
      return lista;
    }

    public List<Pedido> ListarEntregas()
    {
      List<Pedido> lista = new List<Pedido>();
      foreach (var entrega in SetorEntregas)
      {
        lista.Add(entrega.Pedido);
      }
      return lista;
    }

    public List<Estoque> ListarEstoques()
    {
      List<Estoque> lista = new List<Estoque>();

      foreach (var loja in Lojas)
      {
        foreach (var estoque in loja.Estoques)
        {
          lista.Add(estoque);
        }
      }
      return lista;
    }

    public Estoque BuscarEstoque(int codigoProduto)
    {
      Estoque estoque = null;
      foreach (var loja in Lojas)
      {
        if (loja.Estoques.Exists(x => x.Produto.Codigo == codigoProduto))
        {
          estoque = loja.Estoques.Find(x => x.Produto.Codigo == codigoProduto);
        }
      }
      return estoque;
    }
  }
}