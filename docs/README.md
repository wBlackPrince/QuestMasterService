# Техническая документация QuestMaster

Документация подготовлена по исходному коду репозитория на 5 ноября 2026 года. Она описывает фактически реализованное состояние, а не предполагаемую целевую систему.

## Навигация

- [Архитектурный обзор](architecture/overview.md)
- [Контекст системы и диаграмма](architecture/system-context.md)
- [Сервис и слои](architecture/microservices.md)
- [Потоки данных](architecture/data-flow.md)
- [Backend](backend/overview.md) и [детали QuestMasterService](backend/services/quest-master-service.md)
- [API](api/overview.md) и [endpoint-ы](api/endpoints.md)
- [Состояние frontend](frontend/overview.md)
- [Состояние хранения данных](database/overview.md)
- [Настройка разработки](development/setup.md), [конфигурация](development/configuration.md), [тестирование](development/testing.md), [диагностика](development/troubleshooting.md)
- [Архитектурные решения и наблюдения](decisions/architecture-decisions.md)

## Границы достоверности

В репозитории нет frontend-проекта, рабочей Docker/Docker Compose-конфигурации, миграций, `DbContext`, тестовых проектов, CI-конфигурации и реализации аутентификации. В корне есть пустой `compose.yml` без сервисов. Ссылки на PostgreSQL и некоторые application-пакеты подтверждают только подготовку зависимостей, но не работающую интеграцию.
