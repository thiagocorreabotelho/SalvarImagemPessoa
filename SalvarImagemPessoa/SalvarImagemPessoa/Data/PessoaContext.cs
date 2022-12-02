using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalvarImagemPessoa.Models;

    public class PessoaContext : DbContext
    {
        public PessoaContext (DbContextOptions<PessoaContext> options)
            : base(options)
        {
        }

        public DbSet<SalvarImagemPessoa.Models.PessoaModel> PessoaModel { get; set; } = default!;
    }
