namespace Shop.BuisnesLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixAdminRoleAssignment : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            -- Находим ID роли Admin
            DECLARE @adminRoleId UNIQUEIDENTIFIER
            SELECT @adminRoleId = Id FROM Roles WHERE Name = 'Admin'
            
            -- Проверяем, что роль существует
            IF @adminRoleId IS NOT NULL
            BEGIN
                -- Обновляем пользователя Admin
                UPDATE Users 
                SET RoleId = @adminRoleId 
                WHERE Name = 'Admin'
                
                PRINT 'Роль Admin успешно назначена пользователю Admin'
            END
            ELSE
            BEGIN
                PRINT 'Ошибка: Роль Admin не найдена в таблице Roles'
            END
        ");
        }
        
        public override void Down()
        {
            Sql("UPDATE Users SET RoleId = NULL WHERE Name = 'Admin'");
        }
    }
}
