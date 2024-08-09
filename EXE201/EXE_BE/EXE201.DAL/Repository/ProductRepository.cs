using EXE201.DAL.DTOs;
using EXE201.DAL.DTOs.DashboardDTOs;
using EXE201.DAL.DTOs.FeedbackDTOs;
using EXE201.DAL.DTOs.ProductDTOs;
using EXE201.DAL.Interfaces;
using EXE201.DAL.Models;
using LMSystem.Repository.Helpers;
using MCC.DAL.Repository.Implements;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EXE201.DAL.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly EXE201Context _context;

        public ProductRepository(EXE201Context context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDetailDTO>> GetProductsById(int productId)
        {
            return await _context.Products
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductDetails)
                .Include(p => p.Ratings)
                .ThenInclude(r => r.User)
                .Include(p => p.Ratings)
                .ThenInclude(r => r.Feedback)
                .Include(p => p.Category)
                .Where(p => p.ProductId == productId)
                .Select(p => new ProductDetailDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductTitle = p.ProductTitle,
                    ProductDescription = p.ProductDescription,
                    ProductImage = p.ProductImages.Select(p => p.Image.ImageUrl).ToList(),
                    ProductPrice = p.ProductPrice,
                    ProductSize = p.ProductSizes.Select(p => p.Size.SizeName).ToList(),
                    ProductColor = p.ProductColors.Select(p => p.Color.ColorName).ToList(),
                    ProductColorImage = p.ProductColors.Select(p => p.ProductColorImage),
                    ProductStatus = p.ProductStatus,
                    CategoryName = p.Category.CategoryName,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0,
                    RatingsFeedback = p.Ratings.Select(r => new RatingFeedbackDTO
                    {
                        RatingId = r.RatingId,
                        UserId = r.User.UserId,
                        UserName = r.User.UserName,
                        RatingValue = r.RatingValue ?? 0,
                        DateGiven = r.DateGiven,
                        FeedbackComment = r.Feedback.FeedbackComment,
                        FeedbackImage = r.Feedback.FeedbackImage
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ResponeModel> AddProduct(AddProductDTO addProduct)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validate CategoryId
                var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == addProduct.CategoryId);
                if (!categoryExists)
                {
                    return new ResponeModel { Status = "Error", Message = "Invalid CategoryId" };
                }

                var product = new Product
                {
                    ProductName = addProduct.Name,
                    ProductTitle = addProduct.Title,
                    ProductDescription = addProduct.Description,
                    ProductStatus = "Available",
                    ProductPrice = addProduct.Price,
                    CategoryId = addProduct.CategoryId
                };

                // Handle Product Images
                var imageEntities = await _context.Images
                    .Where(i => addProduct.ProductImage.Contains(i.ImageUrl))
                    .ToListAsync();

                var newImageUrls = addProduct.ProductImage.Except(imageEntities.Select(i => i.ImageUrl)).ToList();
                var newImages = newImageUrls.Select(url => new Image { ImageUrl = url }).ToList();

                if (newImages.Any())
                {
                    _context.Images.AddRange(newImages);
                    await _context.SaveChangesAsync(); // Save new images to get their IDs
                    imageEntities.AddRange(newImages);
                }

                product.ProductImages = imageEntities
                    .Select(img => new ProductImage { ImageId = img.ImageId, Product = product }).ToList();

                // Commented out: Handle Product Colors
                /*
                var colorEntities = await _context.Colors
                    .Where(c => addProduct.ExistingColorIds.Select(ec => ec.ColorId).Contains(c.ColorId) || addProduct.ProductColors.Select(pc => pc.ColorName).Contains(c.ColorName))
                    .ToListAsync();

                var newColors = addProduct.ProductColors
                    .Where(pc => !colorEntities.Any(c => c.ColorName == pc.ColorName))
                    .Select(pc => new Color
                    {
                        ColorName = pc.ColorName,
                        HexCode = pc.HexCode
                    }).ToList();

                if (newColors.Any())
                {
                    _context.Colors.AddRange(newColors);
                    await _context.SaveChangesAsync(); // Save new colors to get their IDs
                    colorEntities.AddRange(newColors);
                }

                foreach (var pc in addProduct.ProductColors)
                {
                    var color = colorEntities.First(c => c.ColorName == pc.ColorName);
                    product.ProductColors.Add(new ProductColor
                    {
                        ColorId = color.ColorId,
                        Product = product,
                        ProductColorImage = pc.ColorImage
                    });
                }
                */

                foreach (var existingColor in addProduct.ExistingColorIds)
                {
                    var color = await _context.Colors.FindAsync(existingColor.ColorId);
                    if (color != null)
                    {
                        product.ProductColors.Add(new ProductColor
                        {
                            ColorId = existingColor.ColorId,
                            Product = product,
                            ProductColorImage = existingColor.ColorImage,
                            Color = color
                        });
                    }
                }

                // Commented out: Handle Product Sizes
                /*
                var sizeEntities = await _context.Sizes
                    .Where(s => addProduct.ExistingSizeIds.Contains(s.SizeId) || addProduct.ProductSize.Contains(s.SizeName))
                    .ToListAsync();

                var newSizeNames = addProduct.ProductSize.Except(sizeEntities.Select(s => s.SizeName)).ToList();
                var newSizes = newSizeNames.Select(name => new Size { SizeName = name }).ToList();

                if (newSizes.Any())
                {
                    _context.Sizes.AddRange(newSizes);
                    await _context.SaveChangesAsync(); // Save new sizes to get their IDs
                    sizeEntities.AddRange(newSizes);
                }

                var addedSizeIds = new HashSet<int>(); // Track added size IDs

                foreach (var size in sizeEntities)
                {
                    if (!addedSizeIds.Contains(size.SizeId))
                    {
                        product.ProductSizes.Add(new ProductSize { SizeId = size.SizeId, Product = product });
                        addedSizeIds.Add(size.SizeId);
                    }
                }
                */

                foreach (var sizeId in addProduct.ExistingSizeIds)
                {
                    var size = await _context.Sizes.FindAsync(sizeId);
                    if (size != null)
                    {
                        product.ProductSizes.Add(new ProductSize
                        {
                            SizeId = sizeId,
                            Product = product,
                            Size = size
                        });
                    }
                }

                // Handle Product Detail
                if (addProduct.ProductDetail != null)
                {
                    var productDetail = new ProductDetail
                    {
                        Description = addProduct.ProductDetail.Description,
                        AdditionalInformation = addProduct.ProductDetail.AdditionalInformation,
                        ShippingAndReturns = addProduct.ProductDetail.ShippingAndReturns,
                        SizeChart = addProduct.ProductDetail.SizeChart,
                        Reviews = addProduct.ProductDetail.Reviews,
                        Questions = addProduct.ProductDetail.Questions,
                        VendorInfo = addProduct.ProductDetail.VendorInfo,
                        MoreProducts = addProduct.ProductDetail.MoreProducts,
                        ProductPolicies = addProduct.ProductDetail.ProductPolicies,
                        Product = product
                    };

                    product.ProductDetails = new List<ProductDetail> { productDetail };
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync(); // Commit transaction

                return new ResponeModel
                {
                    Status = "Success",
                    Message = "Added product successfully",
                    DataObject = product
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(); // Rollback transaction in case of an error
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while adding the product" };
            }
        }

        public async Task<ResponeModel> PermanentDeleteProduct(int productId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductSizes)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductDetails)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product not found" };
                }

                // Remove related entities
                _context.ProductColors.RemoveRange(product.ProductColors);
                _context.ProductSizes.RemoveRange(product.ProductSizes);
                _context.ProductImages.RemoveRange(product.ProductImages);
                _context.ProductDetails.RemoveRange(product.ProductDetails);

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResponeModel { Status = "Success", Message = "Product deleted successfully" };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while deleting the product" };
            }
        }




        public async Task<ResponeModel> DeleteProduct(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null && product.ProductStatus == "Available")
            {
                product.ProductStatus = "Not Available";
                Update(product);
                await SaveChangesAsync();
                return new ResponeModel { Status = "Success", Message = "Product delete successfully" };
            }

            if (product.ProductStatus == "Not Available")
            {
                return new ResponeModel { Status = "Error", Message = "Product already delete" };
            }

            return new ResponeModel { Status = "Error", Message = "Product not found" };
        }

        public async Task<ResponeModel> RecoverProduct(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null && product.ProductStatus == "Not Available")
            {
                product.ProductStatus = "Available";
                Update(product);
                await SaveChangesAsync();
                return new ResponeModel { Status = "Success", Message = "Product recover successfully" };
            }

            if (product.ProductStatus == "Available")
            {
                return new ResponeModel { Status = "Error", Message = "Product already Available" };
            }

            return new ResponeModel { Status = "Error", Message = "Product not found" };
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await GetAllAsync();
        }

        public async Task<ProductDetailDTO> GetById(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductDetails)
                .Include(p => p.Ratings)
                .ThenInclude(r => r.User)
                .Include(p => p.Ratings)
                .ThenInclude(r => r.Feedback)
                .Include(p => p.Category)
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDetailDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductTitle = p.ProductTitle,
                    ProductDescription = p.ProductDescription,
                    ProductImageId = p.ProductImages.Select(p => p.Image.ImageId).ToList(),
                    ProductImage = p.ProductImages.Select(p => p.Image.ImageUrl).ToList(),
                    ProductPrice = p.ProductPrice,
                    ProductSizeId = p.ProductSizes.Select(p => p.ProductSizeId).ToList(),
                    ProductSize = p.ProductSizes.Select(p => p.Size.SizeName).ToList(),
                    ProductColorId = p.ProductColors.Select(p => p.ProductColorId).ToList(),
                    ProductColor = p.ProductColors.Select(p => p.Color.ColorName).ToList(),
                    ProductColorImage = p.ProductColors.Select(p => p.ProductColorImage),
                    ProductStatus = p.ProductStatus,
                    CategoryName = p.Category.CategoryName,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0,
                    RatingsFeedback = p.Ratings.Select(r => new RatingFeedbackDTO
                    {
                        RatingId = r.RatingId,
                        UserId = r.User.UserId,
                        UserName = r.User.UserName,
                        RatingValue = r.RatingValue ?? 0,
                        DateGiven = r.DateGiven,
                        FeedbackComment = r.Feedback.FeedbackComment,
                        FeedbackImage = r.Feedback.FeedbackImage
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<ResponeModel> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductImages)
                    .ThenInclude(pi => pi.Image)
                    .Include(p => p.ProductColors)
                    .ThenInclude(pc => pc.Color)
                    .Include(p => p.ProductSizes)
                    .ThenInclude(ps => ps.Size)
                    .FirstOrDefaultAsync(p => p.ProductId == updateProductDTO.ProductId);

                if (product == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product not found" };
                }

                // Update product properties if provided
                if (!string.IsNullOrEmpty(updateProductDTO.Name))
                {
                    product.ProductName = updateProductDTO.Name;
                }

                if (!string.IsNullOrEmpty(updateProductDTO.ProductTitle))
                {
                    product.ProductTitle = updateProductDTO.ProductTitle;
                }

                if (!string.IsNullOrEmpty(updateProductDTO.Description))
                {
                    product.ProductDescription = updateProductDTO.Description;
                }

                if (updateProductDTO.Price.HasValue)
                {
                    product.ProductPrice = updateProductDTO.Price;
                }

                if (updateProductDTO.CategoryId.HasValue)
                {
                    product.CategoryId = updateProductDTO.CategoryId;
                }

                // Update Product Images if provided
                if (updateProductDTO.ProductImage != null && updateProductDTO.ProductImage.Any())
                {
                    product.ProductImages.Clear();

                    var imageEntities = await _context.Images
                        .Where(i => updateProductDTO.ProductImage.Contains(i.ImageUrl))
                        .ToListAsync();

                    var newImageUrls = updateProductDTO.ProductImage.Except(imageEntities.Select(i => i.ImageUrl))
                        .ToList();
                    var newImages = newImageUrls.Select(url => new Image { ImageUrl = url }).ToList();

                    if (newImages.Any())
                    {
                        _context.Images.AddRange(newImages);
                        await _context.SaveChangesAsync();
                        imageEntities.AddRange(newImages);
                    }

                    product.ProductImages = imageEntities
                        .Select(img => new ProductImage { ImageId = img.ImageId, Product = product }).ToList();
                }

                // Update Product Colors if provided
                if (updateProductDTO.ProductColor != null && updateProductDTO.ProductColor.Any())
                {
                    product.ProductColors.Clear();

                    var colorEntities = await _context.Colors
                        .Where(c => updateProductDTO.ProductColor.Contains(c.ColorName))
                        .ToListAsync();

                    var newColorNames = updateProductDTO.ProductColor.Except(colorEntities.Select(c => c.ColorName))
                        .ToList();
                    var newColors = newColorNames.Select(name => new Color { ColorName = name }).ToList();

                    if (newColors.Any())
                    {
                        _context.Colors.AddRange(newColors);
                        await _context.SaveChangesAsync();
                        colorEntities.AddRange(newColors);
                    }

                    product.ProductColors = colorEntities
                        .Select(color => new ProductColor { ColorId = color.ColorId, Product = product }).ToList();
                }

                // Update Product Sizes if provided
                if (updateProductDTO.ProductSize != null && updateProductDTO.ProductSize.Any())
                {
                    product.ProductSizes.Clear();

                    var sizeEntities = await _context.Sizes
                        .Where(s => updateProductDTO.ProductSize.Contains(s.SizeName))
                        .ToListAsync();

                    var newSizeNames = updateProductDTO.ProductSize.Except(sizeEntities.Select(s => s.SizeName))
                        .ToList();
                    var newSizes = newSizeNames.Select(name => new Size { SizeName = name }).ToList();

                    if (newSizes.Any())
                    {
                        _context.Sizes.AddRange(newSizes);
                        await _context.SaveChangesAsync();
                        sizeEntities.AddRange(newSizes);
                    }

                    product.ProductSizes = sizeEntities
                        .Select(size => new ProductSize { SizeId = size.SizeId, Product = product }).ToList();
                }

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return new ResponeModel
                    { Status = "Success", Message = "Product updated successfully", DataObject = product };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the product" };
            }
        }


        public async Task<PagedResponseDTO<ProductListDTO>> GetFilteredProducts(ProductFilterDTO filter)
        {
            var query = _context.Products
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductDetails)
                .Include(p => p.Ratings)
                .Include(p => p.Category)
                .Select(p => new ProductListDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductTitle = p.ProductTitle,
                    ProductDescription = p.ProductDescription,
                    ProductImage = p.ProductImages.Select(pi => pi.Image.ImageUrl).ToList(),
                    ProductStatus = p.ProductStatus,
                    ProductPrice = p.ProductPrice,
                    Category = p.Category.CategoryName,
                    ProductSize = p.ProductSizes.Select(ps => ps.Size.SizeName).ToList(),
                    ProductColor = p.ProductColors.Select(pc => pc.Color.ColorName).ToList(),
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0,
                    ColorCount =
                        p.ProductColors.Select(pc => pc.Color.ColorName).Distinct().Count() // Count of unique colors
                })
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(p =>
                    p.ProductName.Contains(filter.Search) || p.ProductDescription.Contains(filter.Search));
            }

            if (filter.Colors != null && filter.Colors.Any())
            {
                query = query.Where(p => p.ProductColor.Any(color => filter.Colors.Contains(color)));
            }

            if (filter.Sizes != null && filter.Sizes.Any())
            {
                query = query.Where(p => p.ProductSize.Any(size => filter.Sizes.Contains(size)));
            }

            if (filter.Category != null && filter.Category.Any())
            {
                query = query.Where(p => p.Category == filter.Category.FirstOrDefault());
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrice >= filter.MinPrice);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrice <= filter.MaxPrice);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "price":
                        query = filter.Sort
                            ? query.OrderByDescending(p => p.ProductPrice)
                            : query.OrderBy(p => p.ProductPrice);
                        break;
                    case "name":
                        query = filter.Sort
                            ? query.OrderByDescending(p => p.ProductName)
                            : query.OrderBy(p => p.ProductName);
                        break;
                    case "rating":
                        query = filter.Sort
                            ? query.OrderByDescending(p => p.AverageRating)
                            : query.OrderBy(p => p.AverageRating);
                        break;
                    default:
                        query = query.OrderBy(p => p.ProductId);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(p => p.ProductId);
            }

            var totalCount = await query.CountAsync();
            var products = await query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<ProductListDTO>
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount,
                Items = products
            };
        }


        public async Task<IEnumerable<ProductRecommendationDTO>> GetHotProducts(int topN)
        {
            var products = await _context.Products
                .Include(p => p.RentalOrderDetails)
                .Include(p => p.Category)
                .Include(p => p.Ratings)
                .Select(p => new ProductRecommendationDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImage = p.ProductImages.Select(p => p.Image.ImageUrl).ToList(),
                    ProductPrice = p.ProductPrice,
                    ProductSize = p.ProductSizes.Select(p => p.Size.SizeName).ToList(),
                    ProductColor = p.ProductColors.Select(p => p.Color.ColorName).ToList(),
                    ProductStatus = p.ProductStatus,
                    CategoryName = p.Category.CategoryName,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0,
                    NumberOfPurchases = p.RentalOrderDetails.Count
                })
                .OrderByDescending(p => p.NumberOfPurchases)
                .Take(topN)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<ProductRecommendationDTO>> GetNewProducts(int topN)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Ratings)
                .Select(p => new ProductRecommendationDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImage = p.ProductImages.Select(p => p.Image.ImageUrl).ToList(),
                    ProductPrice = p.ProductPrice,
                    ProductSize = p.ProductSizes.Select(p => p.Size.SizeName).ToList(),
                    ProductColor = p.ProductColors.Select(p => p.Color.ColorName).ToList(),
                    ProductStatus = p.ProductStatus,
                    CategoryName = p.Category.CategoryName,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0,
                    CreatedAt = p.CreatedAt
                })
                .OrderByDescending(p => p.CreatedAt)
                .Take(topN)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<ProductRecommendationDTO>> GetHighlyRatedProducts(int topN)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Ratings)
                .Select(p => new ProductRecommendationDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImage = p.ProductImages.Select(p => p.Image.ImageUrl).ToList(),
                    ProductPrice = p.ProductPrice,
                    ProductSize = p.ProductSizes.Select(p => p.Size.SizeName).ToList(),
                    ProductColor = p.ProductColors.Select(p => p.Color.ColorName).ToList(),
                    ProductStatus = p.ProductStatus,
                    CategoryName = p.Category.CategoryName,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0
                })
                .OrderByDescending(p => p.AverageRating)
                .Take(topN)
                .ToListAsync();

            return products;
        }

        public async Task<PagedResponseDTO<ProductListRecommendByCategoryDTO>> GetProductRecommendationsByCategory(
            int productId, ProductPagingRecommendByCategoryDTO filter)
        {
            // Get the category of the given product
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null || product.CategoryId == null)
            {
                return new PagedResponseDTO<ProductListRecommendByCategoryDTO>
                {
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalCount = 0,
                    Items = new List<ProductListRecommendByCategoryDTO>()
                };
            }

            // Get products of the same category excluding the current product
            var query = _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.ProductId != productId)
                .Select(p => new ProductListRecommendByCategoryDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductTitle = p.ProductTitle,
                    ProductDescription = p.ProductDescription,
                    ProductImage = p.ProductImages.Select(pi => pi.Image.ImageUrl).ToList(),
                    ProductPrice = p.ProductPrice,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0
                })
                .AsQueryable();

            var totalCount = await query.CountAsync();
            var products = await query.Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResponseDTO<ProductListRecommendByCategoryDTO>
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalCount = totalCount,
                Items = products
            };
        }

        public async Task<IEnumerable<ProductSuggestionDTO>> GetProductSuggestions(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<ProductSuggestionDTO>();
            }

            var suggestions = await _context.Products
                .Where(p => p.ProductName.Contains(searchTerm))
                .Select(p => new ProductSuggestionDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductImage = p.ProductImages.Select(pi => pi.Image.ImageUrl).FirstOrDefault(),
                    ProductPrice = p.ProductPrice,
                    AverageRating = p.Ratings.Any() ? p.Ratings.Average(r => r.RatingValue ?? 0) : 0
                })
                .ToListAsync();

            return suggestions;
        }

        public async Task<ResponeModel> AddColorToProduct(int productId, int colorId, string ProductColorImage)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductColors)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product not found" };
                }

                var colorExists = await _context.Colors.AnyAsync(c => c.ColorId == colorId);
                if (!colorExists)
                {
                    return new ResponeModel { Status = "Error", Message = "Color not found" };
                }

                if (product.ProductColors.Any(pc =>
                        pc.ColorId == colorId && pc.ProductColorImage.Equals(ProductColorImage)))
                {
                    return new ResponeModel { Status = "Error", Message = "Product already has this color" };
                }

                var productColor = new ProductColor
                {
                    ProductId = productId,
                    ColorId = colorId,
                    ProductColorImage = ProductColorImage
                };

                product.ProductColors.Add(productColor);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Color added to product successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel
                    { Status = "Error", Message = "An error occurred while adding the color to the product" };
            }
        }

        public async Task<ResponeModel> DeleteColorFromProduct(int productId, int colorId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductColors)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product not found" };
                }

                var productColor = product.ProductColors.FirstOrDefault(pc => pc.ColorId == colorId);
                if (productColor == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Color not associated with this product" };
                }

                product.ProductColors.Remove(productColor);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Color removed from product successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel
                    { Status = "Error", Message = "An error occurred while removing the color from the product" };
            }
        }

        public async Task<ResponeModel> AddSizeToProduct(int productId, int sizeId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductSizes)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product not found" };
                }

                var sizeExists = await _context.Sizes.AnyAsync(s => s.SizeId == sizeId);
                if (!sizeExists)
                {
                    return new ResponeModel { Status = "Error", Message = "Size not found" };
                }

                if (product.ProductSizes.Any(ps => ps.SizeId == sizeId))
                {
                    return new ResponeModel { Status = "Error", Message = "Product already has this size" };
                }

                var productSize = new ProductSize
                {
                    ProductId = productId,
                    SizeId = sizeId
                };

                product.ProductSizes.Add(productSize);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Size added to product successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel
                    { Status = "Error", Message = "An error occurred while adding the size to the product" };
            }
        }

        public async Task<ResponeModel> DeleteSizeFromProduct(int productId, int sizeId)
        {
            try
            {
                var product = await _context.Products
                    .Include(p => p.ProductSizes)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product not found" };
                }

                var productSize = product.ProductSizes.FirstOrDefault(ps => ps.SizeId == sizeId);
                if (productSize == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Size not associated with this product" };
                }

                product.ProductSizes.Remove(productSize);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Size removed from product successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel
                    { Status = "Error", Message = "An error occurred while removing the size from the product" };
            }
        }

        public async Task<ResponeModel> UpdateColorImageForProduct(int productId, int colorId, string newColorImage)
        {
            try
            {
                var productColor = await _context.ProductColors
                    .Include(pc => pc.Product)
                    .Include(pc => pc.Color)
                    .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.ColorId == colorId);

                if (productColor == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Product or color not found" };
                }

                productColor.ProductColorImage = newColorImage;

                _context.ProductColors.Update(productColor);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Color image updated successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel
                    { Status = "Error", Message = "An error occurred while updating the color image" };
            }
        }

        public async Task<int> GetTotalItemsInStock()
        {
            return (int)await _context.Inventories.SumAsync(i => i.QuantityAvailable);
        }

        public async Task<List<CategoryOrderCountDTO>> GetMostOrderedProductCategory()
        {
            var categoryCounts = await _context.Products
                .Include(p => p.Category)
                .Select(p => new { p.Category.CategoryName, Count = p.RentalOrderDetails.Count() })
                .ToListAsync();

            var mostOrderedCategories = categoryCounts
                .GroupBy(pc => pc.CategoryName)
                .Select(g => new CategoryOrderCountDTO
                {
                    CategoryName = g.Key,
                    OrderCount = g.Sum(pc => pc.Count)
                })
                .OrderByDescending(g => g.OrderCount)
                .ToList();

            return mostOrderedCategories;
        }

        public async Task<IEnumerable<ProductStockDTO>> GetTotalItemsInStockForEachProduct()
        {
            return await _context.Products
                .Include(p => p.Inventories)
                .Select(p => new ProductStockDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    TotalItemsInStock = (int)p.Inventories.Sum(i => i.QuantityAvailable)
                })
                .ToListAsync();
        }
    }
}