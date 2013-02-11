use master
GO

--CHEQUEAMOS SI LA BASE DE DATOS EXISTE
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'GestionBancaria')
ALTER DATABASE GestionBancaria SET SINGLE_USER WITH ROLLBACK IMMEDIATE
-- LA LINEA ANTERIOR ELIMINA TODAS LAS CONEXIONES EXISTENTES A LA BASE DE DATOS PARA PODER DROPEARLA
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'GestionBancaria')
DROP DATABASE [GestionBancaria] /* ELIMINAMOS LA BASE DE DATOS */
GO


create database GestionBancaria
GO

use GestionBancaria
GO

create table Usuario(Ci int primary key not null,
					 NombreUsuario nvarchar (15) not null unique,
				     Nombre nvarchar (20) not null,
					 Apellido nvarchar (20) not null,
					 Pass nvarchar (20) not null,
					 Activo bit not null
					 )
GO				 
create table Cliente(IdCliente int primary key not null, 
				     Direccion nvarchar(20),
				     foreign key (IdCliente) references Usuario(Ci))
GO
create table Sucursal(IdSucursal int identity primary key, 
					  Nombre nvarchar(20) not null,
					  Direccion nvarchar(50) not null,
					  Activa bit not null)
GO					 				 
create table Prestamo(IdPrestamo int not null, 
					  NumeroSucursal int not null, 
					  IdCliente int not null,
					  Fecha datetime not null,
					  Cuotas int not null, 
					  Cancelado bit not null,
					  Moneda nvarchar(3) not null,
					  Monto float not null,
					  foreign key (NumeroSucursal) references Sucursal(IdSucursal),
					  foreign key (IdCliente) references Cliente(IdCliente),
					  primary key (IdPrestamo, NumeroSucursal)
					  )
GO					  
create table Cotizacion(Fecha datetime not null,
						PrecioVenta float not null, 
						PrecioCompra float not null
						primary key (Fecha))
					  
					
GO
		     
create table TelefonosClientes(IdCliente int, 
							   Tel varchar(50), 
							   primary key(IdCliente,Tel),
							   foreign key(IdCliente) references Cliente(IdCliente))
GO
create table Empleado(IdUsuario int primary key, 
					  IdSucursal int not null,
					  foreign key (IdUsuario) references Usuario(Ci),
					  foreign key (IdSucursal) references Sucursal(IdSucursal))
GO			  
create table Pagos(IdRecibo int identity primary key not null,
				   IdEmpleado int,
				   IdPrestamo int,
				   NumeroSucursal int,
				   Monto float not null,
				   Fecha datetime not null,
				   NumeroCuota int not null,
				   foreign key (IdEmpleado) references Empleado(IdUsuario),
				   foreign key (IdPrestamo,NumeroSucursal) references Prestamo(IdPrestamo,NumeroSucursal))
go
create table Cuenta(IdCuenta int primary key identity (1,1) not null,
					IdSucursal int not null foreign key references Sucursal(IdSucursal),
					Moneda nvarchar(3) not null,
					IdCliente int foreign key references Cliente(IdCliente),
					Saldo float not null)
					
					
GO					
create table Movimiento(IdSucursal int not null references Sucursal(IdSucursal),
						NumeroMovimiento int not null,
						primary key (IdSucursal, NumeroMovimiento),
						Tipo int not null,
						Fecha datetime not null,
						Moneda nvarchar(3) not null,
						ViaWeb bit not null,
						Monto float not null,
						IdCuenta int not null references Cuenta(IdCuenta),
						CiUsuario int not null references Usuario(Ci))
						--Si es movimiento web el CiUsuario  se carga con CiCliente. Si es movimiento dentro entidad se carga CiEmpleado
						-- . No es necesario establecer un campo de tipo bit para definir si es un movimiento web o dentro de entidad
					
GO



					/*Ci int primary key not null,
					 NombreUsuario nvarchar (15) not null unique,
				     Nombre nvarchar (20) not null,
					 Apellido nvarchar (20) not null,
					 Pass nvarchar (20) not null
					 )*/


/*create table Cliente(IdCliente int primary key not null, 
				     Direccion nvarchar(20),
				     foreign key (IdCliente) references Usuario(Ci))*/

create proc AltaCliente
@Ci int,
@NombreUsuario nvarchar(15),
@Nombre nvarchar(20),
@Apellido nvarchar(20),
@Pass nvarchar(20),
@Direccion nvarchar(20)
as
BEGIN

begin tran --insertamos el cliente

	if exists(select * from Usuario where Usuario.Ci=@Ci or Usuario.NombreUsuario = @NombreUsuario)
	begin
		return -3   --Usuario ya Existe
	end
 
	insert into Usuario(Ci,Nombre,Apellido,NombreUsuario,Pass, Activo)
			values(@Ci, @Nombre, @Apellido, @NombreUsuario, @Pass, 1)
			
		if @@error<>0
		begin
		rollback tran
			return -1  --Si no se pudo insertar un Usuario--
		end

	insert into Cliente(IdCliente, Direccion)
			values(@Ci, @Direccion)

		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo insertar un Cliente--
		end

commit tran
END
GO

/*
create table Empleado(IdUsuario int primary key, 
					  IdSucursal int not null,
					  foreign key (IdUsuario) references Usuario(Ci),
					  foreign key (IdSucursal) references Sucursal(IdSucursal))*/
create proc AltaEmpleado
@Ci int,
@NombreUsuario nvarchar(15),
@Nombre nvarchar(20),
@Apellido nvarchar(20),
@Pass nvarchar(20),
@IdSucursal int
as
BEGIN
	if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursal)
	begin
		return -1   --Sucursal no existe
	end
	
	if exists(select * from Usuario where Usuario.Ci=@Ci or Usuario.NombreUsuario = @NombreUsuario)
	begin
		return -3   --Usuario ya Existe
	end
	
	begin tran --insertamos el Empleado
	insert into Usuario(Ci,Nombre,Apellido,NombreUsuario,Pass,Activo)
			values(@Ci, @Nombre, @Apellido, @NombreUsuario, @Pass, 1)
			
		if @@error<>0
		begin
		rollback tran
			return -2  --Si no se pudo insertar un Usuario--
		end

	
	insert into Empleado(IdUsuario, IdSucursal)
			values(@Ci, @IdSucursal)

	if @@error<>0
	begin
    rollback tran
		return -3  --Si no se pudo insertar un Empleado--
	end

commit tran
END
GO

/*create table Sucursal(IdSucursal int identity primary key, 
					  Nombre nvarchar(20) not null,
					  Direccion nvarchar(50))*/
					  
create proc AltaSucursal
@Nombre nvarchar(20),
@Direccion nvarchar(50)
as
BEGIN
if exists(select * from Sucursal where Sucursal.Direccion=@Direccion or Sucursal.Nombre=@Nombre)
	begin
		return -1   --Nombre/direccion de sucursal ya existe en el sistema
	end

begin tran --insertamos la sucursal
	insert into Sucursal(IdSucursal, Nombre)
			values(@Direccion, @Nombre)

	if @@error<>0
	begin
    rollback tran
		return -2  --Si no se pudo insertar sucursal--
	end

commit tran
END
GO

/*create table Prestamo(IdPrestamo int not null, 
					  NumeroSucursal int not null, 
					  Fecha datetime not null,
					  Cuotas int not null, 
					  Cancelado bit not null,
					  Moneda nvarchar(3) not null,
					  Monto float not null,
					  foreign key (NumeroSucursal) references Sucursal(IdSucursal),
					  primary key (IdPrestamo, NumeroSucursal)
					  )*/

create proc AltaPrestamo
@NumeroSucursal int,
@IdCliente int,
@Fecha datetime,
@Cuotas int,
@Moneda nvarchar,
@Monto int
as
BEGIN
if not exists(select * from Sucursal where Sucursal.IdSucursal=@NumeroSucursal)
	begin
		return -1   --sucursal no existe en el sistema
	end
	
if not exists(select * from Cliente where Cliente.IdCliente=@IdCliente)
	begin
		return -2   --cliente no existe en el sistema
	end
	
declare @cantidad int
select @cantidad = COUNT(*) from Prestamo where Prestamo.NumeroSucursal=@NumeroSucursal
set @cantidad = @cantidad+1 /*numero del nuevo prestamo*/

insert into Prestamo(IdPrestamo, NumeroSucursal, IdCliente, Fecha, Cuotas, Moneda, Monto, Cancelado)
			values(@cantidad, @NumeroSucursal, @IdCliente, @Fecha, @Cuotas, @Moneda, @Monto, 0)
			
	if @@error<>0
	begin
		return -3  --Si no se pudo insertar prestamo--
	end

commit tran
END
GO

/********OPCIONAL********/
create proc CantidadPrestamos /*Si quisiera en algun momento saber la cantidad de prestamos de una sucursal*/
as
begin
	declare @cantidad int
	select @cantidad = COUNT(*) from Prestamo
	return @cantidad

end
GO
/**************************/

/*
create table Movimiento(IdSucursal int not null references Sucursal(IdSucursal),
						NumeroMovimiento int not null,
						primary key (IdSucursal, NumeroMovimiento),
						Tipo int not null,
						Fecha datetime not null,
						Moneda nvarchar(3) not null,
						ViaWeb bit not null,
						Monto float not null,
						IdCuenta int not null references Cuenta(IdCuenta),
						CiUsuario int not null references Usuario(Ci))
						--Si es movimiento web el CiUsuario  se carga con CiCliente. Si es movimiento dentro entidad se carga CiEmpleado
						-- . No es necesario establecer un campo de tipo bit para definir si es un movimiento web o dentro de entidad
						*/

create proc AltaMovimiento /*podemos controlar lo de la cotizacion fuera del script*/
@IdSucursal int,
@Tipo int,
@Fecha datetime,
@Moneda nvarchar(3),
@ViaWeb bit,
@Monto float,
@CiUsuario int
as
BEGIN
if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursal)
	begin
		return -1   --sucursal no existe en el sistema
	end
	
if not exists(select * from Usuario where Usuario.Ci=@CiUsuario)
	begin
		return -2   --Usuario no existe en el sistema
	end
	
declare @cantidad int
select @cantidad = COUNT(*) from Movimiento where Movimiento.IdSucursal=@IdSucursal
set @cantidad = @cantidad+1 /*numero del nuevo movimiento*/

insert into Movimiento(IdSucursal,NumeroMovimiento,Tipo,Fecha,Moneda,ViaWeb,Monto)
			values(@IdSucursal, @cantidad, @Tipo, @Fecha, @Moneda,@ViaWeb,@Monto)
			
	if @@error<>0
	begin
		return -3  --Si no se pudo insertar movimiento--
	end

commit tran
END
GO

/*
create table Cotizacion(
						Fecha datetime not null,
						PrecioVenta float not null, 
						PrecioCompra float not null
						primary key (Fecha))*/
						
create proc AltaCotizacion
@Fecha datetime,
@PrecioVenta float,
@PrecioCompra float
as
BEGIN
if exists(select * from Cotizacion where Cotizacion.Fecha = @Fecha)
	begin
		return -1   --Ya se insertó una cotización en el día @Fecha
	end

insert into Cotizacion(Fecha,PrecioCompra,PrecioVenta)
			values(@Fecha,@PrecioCompra,@PrecioVenta)
			
	if @@error<>0
	begin
		return -3  --Si no se pudo insertar la cotización--
	end

commit tran
END
GO

create proc CancelarPrestamo
@IdSucursal int,
@NumeroPrestamo int
as
BEGIN

	update Prestamo set Cancelado=1 where Prestamo.IdPrestamo=@NumeroPrestamo and Prestamo.NumeroSucursal = @IdSucursal
	if @@error<>0
	begin
		return -3  --Si no se pudo insertar el prestamo--
	end
END
GO
/*create table Pagos(IdRecibo int identity primary key not null,
				   IdEmpleado int,
				   IdPrestamo int,
				   NumeroSucursal int,
				   Monto float not null,
				   Fecha datetime not null,
				   NumeroCuota int not null,
				   foreign key (IdEmpleado) references Empleado(IdUsuario),
				   foreign key (IdPrestamo,NumeroSucursal) references Prestamo(IdPrestamo,NumeroSucursal))*/
CREATE PROC AltaPago
@IdEmpleado int,
@IdPrestamo int,
@NumeroSucursal int,
@Monto float,
@Fecha datetime
as
BEGIN
	declare @NumeroCuota int
	select @NumeroCuota = Pagos.NumeroCuota from Pagos where Pagos.IdPrestamo=@IdPrestamo
	set @NumeroCuota = @NumeroCuota+1 /*numero de la nueva cuota a pagar*/

	insert into Pagos(IdEmpleado,IdPrestamo,Fecha,Monto,NumeroCuota,NumeroSucursal)
			values(@IdEmpleado,@IdPrestamo,@Fecha,@Monto,@NumeroCuota,@NumeroSucursal)
		
		if @@ERROR <> 0
		begin
			return -1 /*error no se pudo insertar pago*/
		end
		
		return @@identity /*retorno el numero de IdRecibo*/

END

GO


CREATE PROC spBuscarClientePorCi
@Ci as int

as
begin
select Usuario.*, Cliente.Direccion from Usuario INNER JOIN Cliente ON Usuario.Ci = Cliente.IdCliente
 where Cliente.IdCliente = @Ci
end

go 

create procedure spListarClientes
as
begin
select Usuario.* ,Cliente.Direccion from Usuario INNER JOIN Cliente ON Usuario.Ci = Cliente.IdCliente 
end

GO 
--INSERTAMOS VALORES PREDETERMINADOS
------------------------------------

--SUCURSAL
insert into Sucursal (Nombre,Direccion,Activa) values ('Sucursal Portones','Avda Bolivia 4507',1)



