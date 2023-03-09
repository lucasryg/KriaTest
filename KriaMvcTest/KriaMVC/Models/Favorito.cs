using System;
using System.Collections.Generic;

namespace KriaMVC.Models;

public partial class Favorito
{
    public int FavId { get; set; }

    public int? RepoId { get; set; }

    public virtual Repositorio? Repo { get; set; }
}
