using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.BL.Services;

public class OptionService : IOptionService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public OptionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OptionDTO> GetOption(int id)
    {
        var option = await _unitOfWork.OptionRepository.GetById(id);
        return _mapper.Map<OptionDTO>(option);
    }

    public async Task CreateOption(OptionDTO optionDto)
    {
        var option = _mapper.Map<Option>(optionDto);
        _unitOfWork.OptionRepository.Insert(option);
        await _unitOfWork.Commit();
    }

    public async Task UpdateOption(OptionDTO optionDto)
    {
        var option = _mapper.Map<Option>(optionDto);
        _unitOfWork.OptionRepository.Update(option);
        await _unitOfWork.Commit();
    }

    public async Task DeleteOption(int id)
    {
        _unitOfWork.OptionRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}
