using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Incon.Models;

public partial class InconContext : DbContext
{
    public InconContext()
    {
    }

    public InconContext(DbContextOptions<InconContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Chatsaccount> Chatsaccounts { get; set; }

    public virtual DbSet<Communicationobj> Communicationobjs { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#pragma warning disable CS1030 // Директива #warning
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=incon;Username=postgres;Password=1234;Persist Security Info=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("incon", "accounttype", new[] { "open", "close" })
            .HasPostgresEnum("incon", "chatsaccess", new[] { "norm", "subscriber", "admin", "moder" })
            .HasPostgresEnum("incon", "friendtype", new[] { "waitanswer", "friend", "sbscriber" })
            .HasPostgresEnum("incon", "quiztype", new[] { "cancelsingleans", "cancelmultipleans", "singleans", "multipleans" })
            .HasPostgresEnum("incon", "typemess", new[] { "postfile", "messfile", "postquiz", "messquiz", "status", "postincluded", "messincluded" })
            .HasPostgresEnum("incon", "typeofdelete", new[] { "norm", "admin" });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Accountid).HasName("account_pkey");

            entity.ToTable("account", "incon");

            entity.HasIndex(e => e.Visid, "account_visid_key").IsUnique();

            entity.Property(e => e.Accountid).HasColumnName("accountid");
            entity.Property(e => e.Apartmentnum)
                .HasPrecision(5)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("apartmentnum");
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(2)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("country");
            entity.Property(e => e.Dateofbirthday).HasColumnName("dateofbirthday");
            entity.Property(e => e.Dateofdesign)
                .HasDefaultValueSql("LOCALTIMESTAMP(0)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofdesign");
            entity.Property(e => e.Email)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Housenum)
                .HasPrecision(5)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("housenum");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Profileidref).HasColumnName("profileidref");
            entity.Property(e => e.Region)
                .HasMaxLength(40)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("region");
            entity.Property(e => e.Street)
                .HasMaxLength(40)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("street");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("surname");
            entity.Property(e => e.Visid)
                .HasMaxLength(50)
                .HasColumnName("visid");

            entity.HasOne(d => d.ProfileidrefNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Profileidref)
                .HasConstraintName("account_profileidref_fkey");

            entity.HasMany(d => d.Accountfriendidrefs).WithMany(p => p.Accountidrefs)
                .UsingEntity<Dictionary<string, object>>(
                    "Accounttoaccount",
                    r => r.HasOne<Account>().WithMany()
                        .HasForeignKey("Accountfriendidref")
                        .HasConstraintName("accounttoaccount_accountfriendidref_fkey"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("Accountidref")
                        .HasConstraintName("accounttoaccount_accountidref_fkey"),
                    j =>
                    {
                        j.HasKey("Accountidref", "Accountfriendidref").HasName("accounttoaccount_pkey");
                        j.ToTable("accounttoaccount", "incon");
                        j.IndexerProperty<int>("Accountidref").HasColumnName("accountidref");
                        j.IndexerProperty<int>("Accountfriendidref").HasColumnName("accountfriendidref");
                    });

            entity.HasMany(d => d.Accountidrefs).WithMany(p => p.Accountfriendidrefs)
                .UsingEntity<Dictionary<string, object>>(
                    "Accounttoaccount",
                    r => r.HasOne<Account>().WithMany()
                        .HasForeignKey("Accountidref")
                        .HasConstraintName("accounttoaccount_accountidref_fkey"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("Accountfriendidref")
                        .HasConstraintName("accounttoaccount_accountfriendidref_fkey"),
                    j =>
                    {
                        j.HasKey("Accountidref", "Accountfriendidref").HasName("accounttoaccount_pkey");
                        j.ToTable("accounttoaccount", "incon");
                        j.IndexerProperty<int>("Accountidref").HasColumnName("accountidref");
                        j.IndexerProperty<int>("Accountfriendidref").HasColumnName("accountfriendidref");
                    });
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Chatid).HasName("chat_pkey");

            entity.ToTable("chat", "incon");

            entity.Property(e => e.Chatid).HasColumnName("chatid");
            entity.Property(e => e.Dateofdesign)
                .HasDefaultValueSql("LOCALTIMESTAMP(0)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofdesign");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Chatsaccount>(entity =>
        {
            entity.HasKey(e => new { e.Chatidref, e.Accountidref }).HasName("chatsaccount_pkey");

            entity.ToTable("chatsaccount", "incon");

            entity.Property(e => e.Chatidref).HasColumnName("chatidref");
            entity.Property(e => e.Accountidref).HasColumnName("accountidref");
            entity.Property(e => e.Dateofadd)
                .HasDefaultValueSql("LOCALTIMESTAMP(0)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofadd");

            entity.HasOne(d => d.AccountidrefNavigation).WithMany(p => p.Chatsaccounts)
                .HasForeignKey(d => d.Accountidref)
                .HasConstraintName("chatsaccount_accountidref_fkey");

            entity.HasOne(d => d.ChatidrefNavigation).WithMany(p => p.Chatsaccounts)
                .HasForeignKey(d => d.Chatidref)
                .HasConstraintName("chatsaccount_chatidref_fkey");
        });

        modelBuilder.Entity<Communicationobj>(entity =>
        {
            entity.HasKey(e => e.Communicationobjid).HasName("communicationobj_pkey");

            entity.ToTable("communicationobj", "incon");

            entity.Property(e => e.Communicationobjid).HasColumnName("communicationobjid");
            entity.Property(e => e.Chatidref).HasColumnName("chatidref");
            entity.Property(e => e.Creatoraccountidref).HasColumnName("creatoraccountidref");
            entity.Property(e => e.Dateofdesign)
                .HasDefaultValueSql("LOCALTIMESTAMP(0)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofdesign");
            entity.Property(e => e.Text)
                .HasDefaultValueSql("''::text")
                .HasColumnName("text");

            entity.HasOne(d => d.ChatidrefNavigation).WithMany(p => p.Communicationobjs)
                .HasForeignKey(d => d.Chatidref)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("communicationobj_chatidref_fkey");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Profileid).HasName("profile_pkey");

            entity.ToTable("profile", "incon");

            entity.HasIndex(e => e.Login, "profile_login_key").IsUnique();

            entity.HasIndex(e => e.Mainemail, "profile_mainemail_key").IsUnique();

            entity.HasIndex(e => e.Mainphone, "profile_mainphone_key").IsUnique();

            entity.Property(e => e.Profileid).HasColumnName("profileid");
            entity.Property(e => e.Apartmentnum)
                .HasPrecision(5)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("apartmentnum");
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(2)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("country");
            entity.Property(e => e.Dateofbirthday).HasColumnName("dateofbirthday");
            entity.Property(e => e.Dateofdesign)
                .HasDefaultValueSql("LOCALTIMESTAMP(0)")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofdesign");
            entity.Property(e => e.Housenum)
                .HasPrecision(5)
                .HasDefaultValueSql("NULL::numeric")
                .HasColumnName("housenum");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Mainemail)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnType("character varying")
                .HasColumnName("mainemail");
            entity.Property(e => e.Mainphone)
                .HasMaxLength(12)
                .HasColumnName("mainphone");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(100)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Region)
                .HasMaxLength(40)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("region");
            entity.Property(e => e.Street)
                .HasMaxLength(40)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("street");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
