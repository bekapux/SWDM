export interface ProductDto {
  id: number;
  slug: string;
  name: string;
  shortDescription: string;
  fullDescription: string;
  productImages: ProductImageDto[];
  productCategories: string[];
  productSpecs: ProductSpecDTO[];

  discount: number;
  price: number;
  order: number;
}

export interface ProductImageDto {
  url: string;
  id: number;
  order: number;
  isMainImage: boolean;
}

export interface ProductSpecDTO {
  specName: string;
  specValue: string;
}
