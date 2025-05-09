using EncomendasApi.Models;

namespace EncomendasApi.Services;

public class EncomendaService
{
    private static List<Encomenda> _encomendas = new();
    private static int _nextId = 1;

    public List<Encomenda> GetAll() => _encomendas;

    public Encomenda? GetById(int id) => _encomendas.FirstOrDefault(e => e.Id == id);

    public Encomenda Add(Encomenda encomenda)
    {
        encomenda.Id = _nextId++;
        _encomendas.Add(encomenda);
        return encomenda;
    }

    public bool Update(int id, Encomenda encomendaAtualizada)
    {
        var index = _encomendas.FindIndex(e => e.Id == id);
        if (index == -1) return false;

        encomendaAtualizada.Id = id;
        _encomendas[index] = encomendaAtualizada;
        return true;
    }

    public bool Delete(int id)
    {
        var encomenda = GetById(id);
        if (encomenda == null) return false;

        _encomendas.Remove(encomenda);
        return true;
    }
}
