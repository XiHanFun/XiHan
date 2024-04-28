## XiHan.Domain.Core

曦寒领域核心组件库

## 如何使用

项目引用此 Nuget 包

依赖注入

```csharp
services.RunModuleInitializers(configuration)
```

## 包含功能

### 实体

> 通用实体接口

IEntity
IExpirable
IHasAudited
IHasCreated
IHasDeleted
IHasModified
IMustHaveOrg
IMustHaveTenant
ISoftDeleted

> 通用实体基类

BaseAuditedEntity
BaseCreatedEntity
BaseDeletedEntity
BaseEntity
BaseModifiedEntity
BaseOrgEntity
BaseTenantEntity

### 值对象

> 值对象基类

ValueObject

### 聚合根

> 聚合根接口

IAggregateRoot

> 聚合根基类

AggregateRoot

### 仓储

> 通用仓储接口

IBaseRepository

### 服务

> 通用服务接口

IBaseService

### 事件

> 通用领域事件领域事件接口

IBaseDomainEvent

> 通用领域事件领域事件基类

BaseDomainEvent

### 规约

### 异常