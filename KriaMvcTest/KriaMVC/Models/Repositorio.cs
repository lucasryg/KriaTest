using System;
using System.Collections.Generic;

namespace KriaMVC.Models;

public partial class Repositorio
{
    public int RepoId { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public string Linguagem { get; set; } = null!;

    public DateTime UltimaAtt { get; set; }

    public string DonoRepositorio { get; set; } = null!;

    public virtual ICollection<Favorito> Favoritos { get; } = new List<Favorito>();
}