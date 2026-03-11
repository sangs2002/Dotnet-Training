namespace Library.Management.Infrastructure.Data;
public partial class LibraryDbContext : DbContext, ILibraryDbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Books> Books { get; set; }

    public virtual DbSet<Borrowing> Borrowings { get; set; }

    public virtual DbSet<Fines> Fines { get; set; }

    public virtual DbSet<Members> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Books>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207F93D7187");

            entity.HasIndex(e => e.ISBN, "UQ__Books__447D36EA617B381A").IsUnique();

            entity.Property(e => e.Author)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Genre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(e => e.BorrowingId).HasName("PK__Borrowin__6CD933D76BE6B119");

            entity.Property(e => e.BorrowedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Status).IsRequired();

            entity.HasOne(d => d.Book).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Borrowings_Books");

            entity.HasOne(d => d.Member).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Borrowings_Members");
        });

        modelBuilder.Entity<Fines>(entity =>
        {
            entity.HasKey(e => e.FineId).HasName("PK__Fines__9D4A9B2C38A11CD5");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Borrowing).WithMany(p => p.Fines)
                .HasForeignKey(d => d.BorrowingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fines_Borrowings");

            entity.HasOne(d => d.Members).WithMany(p => p.Fines)
            .HasForeignKey(d => d.MemberId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Fines_Members");
        });

        modelBuilder.Entity<Members>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B18461C9D35");

            entity.HasIndex(e => e.Email, "UQ__Members__A9D105346C76A72A").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MembershipType).IsRequired();
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
