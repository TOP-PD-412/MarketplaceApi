namespace Shared.Users;

public enum UserStatuses
{
    Active,
    Banned,
    Deleted
}


// Users СОЗДАН / АКТИВНЫЙ / ЗАБАНЕН / УДАЛЕН
//                 +111
//                            +222
//                                      +333
//       +444

// Users АКТИВНЫЙ / ЗАБАНЕН / УДАЛЕН
// Requests НА РЕГИСТРАЦИЮ, НА ДОБАВЛЕНИЕ ТОВАРА, НА КОММЕНТАРИЙ, НА ОБНОВЛЕНИЕ ТОВАРА
// ОЖИДАЕТ
// ПРИНЯТА
// ОТКЛОНЕНА