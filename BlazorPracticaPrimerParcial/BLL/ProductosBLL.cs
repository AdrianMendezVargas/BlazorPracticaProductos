using BlazorPracticaPrimerParcial.Data;
using BlazorPracticaPrimerParcial.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPracticaPrimerParcial.BLL {
    public class ProductosBLL {

        public async static Task<bool> Guardar(Producto producto) {
            if (!await Existe(producto.Id))
                return await Insertar(producto);
            else
                return await Modificar(producto);
        }

        private async static Task<bool> Insertar(Producto producto) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Productos.Add(producto);
                paso = await contexto.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<bool> Modificar(Producto producto) {
            bool paso = false;
            Contexto contexto = new Contexto();

            try {
                contexto.Entry(producto).State = EntityState.Modified;

                paso = await contexto.SaveChangesAsync() > 0;

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }
            return paso;
        }

        public async static Task<bool> Eliminar(int id) {
            bool paso = false;
            Contexto contexto = new Contexto();
            try {
                var producto = contexto.Productos.Find(id);

                if (producto != null) {
                    contexto.Productos.Remove(producto);
                    paso = await contexto.SaveChangesAsync() > 0;
                }
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return paso;
        }

        public async static Task<Producto> Buscar(int id) {
            Contexto contexto = new Contexto();
            Producto producto;

            try {
                producto = await contexto.Productos
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return producto;
        }


        public async static Task<bool> Existe(int id) {
            Contexto contexto = new Contexto();
            bool encontrado = false;

            try {
                encontrado = await contexto.Productos.AnyAsync(e => e.Id == id);
            } catch (Exception) {
                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return encontrado;
        }

        public async static Task<List<Producto>> GetProductos() {
            Contexto contexto = new Contexto();

            List<Producto> productos = new List<Producto>();
            await Task.Delay(01); //Para dar tiempo a renderizar el mensaje de carga

            try {
                productos = await contexto.Productos.ToListAsync();

            } catch (Exception) {

                throw;
            } finally {
                await contexto.DisposeAsync();
            }

            return productos;

        }

    }
}
