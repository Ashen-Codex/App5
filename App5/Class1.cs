using System;
using System.Collections.Generic;
using System.Text;

namespace App5
{
    public abstract class Notification
    {
        public abstract string GetMessage();
        public abstract string GetColor();

        public virtual string Display()
        {
            return GetMessage();
        }
    }

    public class ErrorNotification : Notification
    {
        public override string GetMessage() => "Ошибка: произошла критическая ошибка!";
        public override string GetColor() => "Red";
    }

    public class WarningNotification : Notification
    {
        public override string GetMessage() => "Предупреждение: возможна нестабильная работа.";
        public override string GetColor() => "Orange";
    }

    public class InfoNotification : Notification
    {
        public override string GetMessage() => "Информация: операция выполнена успешно.";
        public override string GetColor() => "Blue";
    }

    public class NotificationFactory
    {
        public static Notification CreateNotification(string type)
        {
            switch (type)
            {
                case "error":
                    return new ErrorNotification();
                case "warning":
                    return new WarningNotification();
                case "info":
                    return new InfoNotification();
                default:
                    throw new System.ArgumentException("Неизвестный тип уведомления");
            }
        }
    }
}
