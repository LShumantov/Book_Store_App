namespace BookStoreApp.Helper
{
    using AutoMapper;
    using BookStoreApp.Dto;
    using BookStoreApp.Models;
    public class MappingPlofiles : Profile
    {
        public MappingPlofiles() 
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();          
            CreateMap<WareHouse, WareHouseDto>();
            CreateMap<WareHouseDto, WareHouse>();
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto,Author>();
            CreateMap<ShoppingBasket, ShoppingBasketDto>();
            CreateMap<ShoppingBasketDto, ShoppingBasket>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>();           
        }    
    }
}
