using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MakeMemberPropertiesNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    biography = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    avartar = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__authors__86516BCF7E187956", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "book_groups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__book_gro__D57795A063510C0C", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "bookshelves",
                columns: table => new
                {
                    shelf_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    column_number = table.Column<int>(type: "int", nullable: false),
                    row_number = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    shelf_number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bookshel__E33A5B7C58D0B7C2", x => x.shelf_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_code = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__D54EE9B481A10DEA", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "fees",
                columns: table => new
                {
                    fee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fee_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    min_price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    max_price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__fees__A19C8AFB1BA01D69", x => x.fee_id);
                });

            migrationBuilder.CreateTable(
                name: "member_groups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    fee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__member_g__D57795A00F48C8A8", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "members",
                columns: table => new
                {
                    member_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    id_card_number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    profile_picture = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    membership_fee = table.Column<int>(type: "int", nullable: true),
                    reset_pin = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    reset_pin_expire = table.Column<DateTime>(type: "datetime", nullable: true),
                    role = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    membership_fee_due_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    balance = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__members__B29B8534C892D180", x => x.member_id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    publisher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__publishe__3263F29D19516640", x => x.publisher_id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staff_id = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__staff__1963DD9CC2B536E8", x => x.staff_id);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    supplier_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    contact_info = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__supplier__6EE594E838C5466C", x => x.supplier_id);
                });

            migrationBuilder.CreateTable(
                name: "book_access_for_member_groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    book_group_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__book_acc__3213E83FAA555A86", x => x.id);
                    table.ForeignKey(
                        name: "FK_BookAccessForMemberGroups_BookGroups",
                        column: x => x.book_group_id,
                        principalTable: "book_groups",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "FK_BookAccessForMemberGroups_MemberGroups",
                        column: x => x.group_id,
                        principalTable: "member_groups",
                        principalColumn: "group_id");
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__notifica__E059842FD9DF56BB", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_Notifications_Members",
                        column: x => x.user_id,
                        principalTable: "members",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    publish_year = table.Column<int>(type: "int", nullable: false),
                    max_copies_per_shelf = table.Column<int>(type: "int", nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    supplier_id = table.Column<int>(type: "int", nullable: false),
                    publisher_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    available_copies = table.Column<int>(type: "int", nullable: false),
                    damage_fee = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    warehouse = table.Column<bool>(type: "bit", nullable: false),
                    cover = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: false),
                    pdf_link = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    views = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__books__490D1AE1E874427C", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_Books_Authors",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "author_id");
                    table.ForeignKey(
                        name: "FK_Books_Categories",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id");
                    table.ForeignKey(
                        name: "FK_Books_Publishers",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "publisher_id");
                    table.ForeignKey(
                        name: "FK_Books_Suppliers",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "supplier_id");
                });

            migrationBuilder.CreateTable(
                name: "book_copies",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false),
                    copies_number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_available = table.Column<bool>(type: "bit", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    warehouse = table.Column<bool>(type: "bit", nullable: false),
                    shelf_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__book_cop__490D1AE19BD3C2BB", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_BookCopies_Books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_BookCopies_Bookshelves",
                        column: x => x.shelf_id,
                        principalTable: "bookshelves",
                        principalColumn: "shelf_id");
                });

            migrationBuilder.CreateTable(
                name: "book_in_groups",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    group_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__book_in___3213E83F683D11D4", x => x.id);
                    table.ForeignKey(
                        name: "FK_BookInGroups_BookGroups",
                        column: x => x.group_id,
                        principalTable: "book_groups",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "FK_BookInGroups_Books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                });

            migrationBuilder.CreateTable(
                name: "favorites_list",
                columns: table => new
                {
                    favorites_list_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__favorite__DD78BFC57543047C", x => x.favorites_list_id);
                    table.ForeignKey(
                        name: "FK_FavoritesList_Books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_FavoritesList_Members",
                        column: x => x.user_id,
                        principalTable: "members",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "reading_progress",
                columns: table => new
                {
                    progress_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    member_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    reading_progress = table.Column<int>(type: "int", nullable: true),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reading___49B3D8C157882949", x => x.progress_id);
                    table.ForeignKey(
                        name: "FK_ReadingProgress_Books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_ReadingProgress_Members",
                        column: x => x.member_id,
                        principalTable: "members",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reviews__60883D90C64EC143", x => x.review_id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_Reviews_Members",
                        column: x => x.user_id,
                        principalTable: "members",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "liquidated_books",
                columns: table => new
                {
                    liquidated_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: false),
                    copy_id = table.Column<int>(type: "int", nullable: false),
                    liquidated_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    liquidated_by = table.Column<int>(type: "int", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__liquidat__BEBBDCCCE09DCC65", x => x.liquidated_id);
                    table.ForeignKey(
                        name: "FK_LiquidatedBooks_BookCopies",
                        column: x => x.copy_id,
                        principalTable: "book_copies",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_LiquidatedBooks_Books",
                        column: x => x.book_id,
                        principalTable: "books",
                        principalColumn: "book_id");
                });

            migrationBuilder.CreateTable(
                name: "loans",
                columns: table => new
                {
                    loan_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    copy_id = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    loan_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    fine = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    borrow_fee = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__loans__A1F79554CC5BBD8B", x => x.loan_id);
                    table.ForeignKey(
                        name: "FK_Loans_BookCopies",
                        column: x => x.copy_id,
                        principalTable: "book_copies",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK_Loans_Members",
                        column: x => x.user_id,
                        principalTable: "members",
                        principalColumn: "member_id");
                });

            migrationBuilder.CreateTable(
                name: "penalties",
                columns: table => new
                {
                    penalty_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loan_id = table.Column<int>(type: "int", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_by = table.Column<int>(type: "int", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by = table.Column<int>(type: "int", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    cover_tear = table.Column<int>(type: "int", nullable: false),
                    spine_damage = table.Column<int>(type: "int", nullable: false),
                    page_loss = table.Column<int>(type: "int", nullable: false),
                    writing = table.Column<int>(type: "int", nullable: false),
                    over_due = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__penaltie__0AAEFF0BE5A63B9C", x => x.penalty_id);
                    table.ForeignKey(
                        name: "FK_Penalties_Loans",
                        column: x => x.loan_id,
                        principalTable: "loans",
                        principalColumn: "loan_id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__authors__86516BCE3D9E071B",
                table: "authors",
                column: "author_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_access_for_member_groups_book_group_id",
                table: "book_access_for_member_groups",
                column: "book_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_access_for_member_groups_group_id",
                table: "book_access_for_member_groups",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "UQ__book_acc__3213E83EF167CF2F",
                table: "book_access_for_member_groups",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_copies_shelf_id",
                table: "book_copies",
                column: "shelf_id");

            migrationBuilder.CreateIndex(
                name: "UQ__book_cop__490D1AE0FA28F52C",
                table: "book_copies",
                column: "book_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__book_gro__D57795A16C06433E",
                table: "book_groups",
                column: "group_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_book_in_groups_book_id",
                table: "book_in_groups",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_in_groups_group_id",
                table: "book_in_groups",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "UQ__book_in___3213E83EE3F5513A",
                table: "book_in_groups",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_books_author_id",
                table: "books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_category_id",
                table: "books",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_publisher_id",
                table: "books",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_books_supplier_id",
                table: "books",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "UQ__books__490D1AE02AB25E2D",
                table: "books",
                column: "book_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__bookshel__E33A5B7D2BD61285",
                table: "bookshelves",
                column: "shelf_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__categori__D54EE9B5110FC622",
                table: "categories",
                column: "category_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_favorites_list_book_id",
                table: "favorites_list",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorites_list_user_id",
                table: "favorites_list",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__favorite__DD78BFC4A567845B",
                table: "favorites_list",
                column: "favorites_list_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__fees__A19C8AFA04EA1C6D",
                table: "fees",
                column: "fee_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_liquidated_books_book_id",
                table: "liquidated_books",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_liquidated_books_copy_id",
                table: "liquidated_books",
                column: "copy_id");

            migrationBuilder.CreateIndex(
                name: "UQ__liquidat__BEBBDCCD70DB55A3",
                table: "liquidated_books",
                column: "liquidated_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loans_copy_id",
                table: "loans",
                column: "copy_id");

            migrationBuilder.CreateIndex(
                name: "IX_loans_user_id",
                table: "loans",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__loans__A1F79555F3668C26",
                table: "loans",
                column: "loan_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__member_g__D57795A11A31DE30",
                table: "member_groups",
                column: "group_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__members__B29B853542E36638",
                table: "members",
                column: "member_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__notifica__E059842E4CC60288",
                table: "notifications",
                column: "notification_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_penalties_loan_id",
                table: "penalties",
                column: "loan_id");

            migrationBuilder.CreateIndex(
                name: "UQ__penaltie__0AAEFF0A96B0BBF6",
                table: "penalties",
                column: "penalty_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__publishe__3263F29C5EBE5068",
                table: "publishers",
                column: "publisher_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reading_progress_book_id",
                table: "reading_progress",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_reading_progress_member_id",
                table: "reading_progress",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "UQ__reading___49B3D8C08DF0930C",
                table: "reading_progress",
                column: "progress_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_book_id",
                table: "reviews",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__reviews__60883D916980E7F5",
                table: "reviews",
                column: "review_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__staff__1963DD9DCEBD7C88",
                table: "staff",
                column: "staff_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__supplier__6EE594E92367A745",
                table: "suppliers",
                column: "supplier_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_access_for_member_groups");

            migrationBuilder.DropTable(
                name: "book_in_groups");

            migrationBuilder.DropTable(
                name: "favorites_list");

            migrationBuilder.DropTable(
                name: "fees");

            migrationBuilder.DropTable(
                name: "liquidated_books");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "penalties");

            migrationBuilder.DropTable(
                name: "reading_progress");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "member_groups");

            migrationBuilder.DropTable(
                name: "book_groups");

            migrationBuilder.DropTable(
                name: "loans");

            migrationBuilder.DropTable(
                name: "book_copies");

            migrationBuilder.DropTable(
                name: "members");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "bookshelves");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "suppliers");
        }
    }
}
