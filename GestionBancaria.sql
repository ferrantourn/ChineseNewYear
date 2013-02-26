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
 
	insert into Usuario(Ci,Nombre,Apellido,NombreUsuario, Pass, Activo)
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
	insert into Sucursal(IdSucursal, Nombre, Activa)
			values(@Direccion, @Nombre, 1)

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


END
GO

/********OPCIONAL********/
create proc CantidadPrestamos /*Si quisiera en algun momento saber la cantidad de prestamos de una sucursal*/
@NumeroSucursal int
as
begin
	declare @cantidad int
	select @cantidad = COUNT(*) from Prestamo where NumeroSucursal = @NumeroSucursal
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
if not exists(select * from Sucursal where Sucursal.IdSucursal=@IdSucursal and Sucursal.Activa = 0)
	begin
		return -1   --sucursal no existe en el sistema o est� inactiva
	end
	
if not exists(select * from Usuario where Usuario.Ci=@CiUsuario and Usuario.Activo = 0)
	begin
		return -2   --Usuario no existe en el sistema o est� inactivo
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
		return -1   --Ya se insert� una cotizaci�n en el d�a @Fecha
	end

insert into Cotizacion(Fecha,PrecioCompra,PrecioVenta)
			values(@Fecha,@PrecioCompra,@PrecioVenta)
			
	if @@error<>0
	begin
		return -3  --Si no se pudo insertar la cotizaci�n--
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
	declare @NumeroCuota int=0
	select @NumeroCuota = Pagos.NumeroCuota from Pagos where Pagos.IdPrestamo=@IdPrestamo
	set @NumeroCuota = @NumeroCuota+1 /*numero de la nueva cuota a pagar*/
	
	if exists(select * from Usuario where usuario.Ci =@IdEmpleado and Activo = 0)
	begin
		return -1 --usuario empleado no existe o est� inactivo
	end
	

	insert into Pagos(IdEmpleado,IdPrestamo,Fecha,Monto,NumeroCuota,NumeroSucursal)
			values(@IdEmpleado,@IdPrestamo,@Fecha,@Monto,@NumeroCuota,@NumeroSucursal)
		
		if @@ERROR <> 0
		begin
			return -1 /*error no se pudo insertar pago*/
		end
		
		return @@identity /*retorno el numero de IdRecibo*/

END

GO

CREATE PROC spListarUltimosPagos --muestra el id prestamo sin cancelar y el �ltimo realizado para dicho prestamo
@IdSucursal int
as
BEGIN

	if exists(select * from Sucursal where IdSucursal=@IdSucursal and Activa = 0)
	begin
		return -1 --Sucursal no existe o est� inactiva
	end
	select * from Prestamo INNER JOIN
		(select IdPrestamo, MAX(Pagos.Fecha) maxfecha from Pagos group by IdPrestamo) MP on Prestamo.IdPrestamo = MP.IdPrestamo 
		INNER JOIN Pagos on Pagos.IdPrestamo=MP.IdPrestamo and Pagos.Fecha = MP.maxfecha	
		where Prestamo.Cancelado = 0

END
GO


/*
CREATE PROC spListarAtrasados --sin terminar
@IdSucursal int,
@FechaActual datetime
as
BEGIN

select Prestamo.IdPrestamo from Pagos,Prestamo where 
												(((DATEPART(day,Prestamo.Fecha) - DATEPART(day,@FechaActual)<0) 
													and (DATEPART(month,Pagos.Fecha) - DATEPART(month,@FechaActual)=1) 
													or ((DATEPART(month,Pagos.Fecha) - DATEPART(month,@FechaActual)<1)))
												and (Pagos.NumeroSucursal=@IdSucursal) and (Prestamo.Cancelado=0))

/*Condiciones que se deben cumplir para mostar los prestamos atrasados de pago:

1-el prestamo fue pedido un dia X, por ejemplo 12, 
y estamos a un 13 y hay UN MES de diferencia entre pago anterior y fecha actual,
en este caso estar�a atrasdo 1 d�a

2-Si hay m�s de un mes de diferencia entre el �ltimo pago y el d�a de hoy, 
ejemplo: ultimo pago 11 de julio, dia actual 10 de diciembre, esta muy atrasado.

3-el prestamo no esta cancelado, y el prestamo es de la IdSucursal pasada por parametro
condiciones del sp*/ 

END
GO*/


CREATE PROC spBuscarClientePorCi -- falta filtrar inactivos, ser�a agregando and Usuario.Activo=0
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


/*Bajas, de empleado, cliente, usuario, desactivacion de sucursal, */




--INSERTAMOS VALORES PREDETERMINADOS
------------------------------------

--SUCURSAL
insert into Sucursal (Nombre,Direccion,Activa) values ('Sucursal Portones','Avda Bolivia 4507',1)
exec AltaEmpleado @IdSucursal=1, @Ci=1234567,@NombreUsuario='ElGaucho', @Nombre='Roberto',@Apellido='Gonzales',@Pass='1234'
exec AltaCliente @Direccion='aca', @Nombre='Amalfi', @Apellido='Marini',@Pass='jojojo', @Ci=3446586, @NombreUsuario = 'fito'
exec AltaPrestamo @Monto=10000, @Cuotas=10, @Moneda='UYU', @Fecha='01/01/1998', @IdCliente=3446586, @NumeroSucursal = 1
exec AltaPago @Fecha='01/02/1981', @Monto=1000, @NumeroSucursal=1, @IdEmpleado = 1234567, @IdPrestamo=1

exec spListarUltimosPagos 1

select * from Pagos
select * from Cliente
select * from Sucursal
select * from Usuario
select * from Prestamo




