import axios from "axios";
import Product from "../Interface/Product.ts";

const API_BASE_URL = "https://localhost:44364/api";

export const productService = {
  getProducts: async (): Promise<Product[]> => {
    try {
      const response = await axios.get<Product[]>(`${API_BASE_URL}/products`);
      return response.data;
    } catch (error) {
      console.error("Error fetching product data:", error);
      throw error;
    }
  },
  // Add more methods for other API endpoints as needed
};


