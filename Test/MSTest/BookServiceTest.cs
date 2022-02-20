using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GenFu;
using ShopService.Book.Application.DTO;
using ShopService.Book.Domain.Entities;
using System;
using ShopService.Book.Infraestructure.Persistance.Repository;
using AutoMapper;
using ShopService.Book.Application.Query;
using ShopService.Book.Infraestructure.Persistance.Interface;

namespace MSTest;

[TestClass]
public class BookServiceTest
{
    [TestMethod]
    public void TestMethod1()
    {

    }

    private IEnumerable<BookMaterial> getBookData() 
    {
        //A.Configure
        //(IEnumerable<BookMaterialDTO>)A.Configure<BookMaterial>()
        A.Configure<BookMaterial>()
            .Fill(d => d.name).AsArticleTitle()
            .Fill(d => d.guid, () => {return Guid.NewGuid(); });

        var list = A.ListOf<BookMaterial>(50);
        list[0].guid = Guid.Empty; // For create an static GUID for query

        return list;
    }

    public void GetBooks()
    {
        // Arrange, Act and Assert (AAA) Pattern
        var mockRepository = new Mock<IRepositoryBaseAsync<BookMaterial>>();
        var mockMapper = new Mock<IMapper>();

        var handler = new GetBooksQueryHandler(mockRepository.Object, mockMapper.Object);

        var test = handler.
    }
}