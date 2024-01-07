import React, { useEffect, useState } from "react";
import { productService } from "../../Service/productService.ts";
import Product from "../../Interface/Product.ts";
import "./ProductList.css";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Toolbar } from "primereact/toolbar";
import leftToolbarTemplate from "../ToolbarTemplate/leftToolbarTemplate.tsx";
import { rightToolbarTemplate } from "../ToolbarTemplate/rightToolbarTemplate.tsx";
import {
  actionBodyTemplate,
  imageBodyTemplate,
  ratingBodyTemplate,
  statusBodyTemplate,
} from "../BodyTemplate.tsx";
import { Dialog } from "primereact/dialog";
import { Button } from "primereact/button";

const ProductList = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProducts, setselectedProducts] = useState<Product>()
  const [loading, setLoading] = useState(true);
  const [deleteProductDialog, setDeleteProductDialog] = useState(false);
  const [product, setProduct] = useState<Product>();
  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await productService.getProducts();
        setProducts(data);
        setLoading(false);
        console.log(data);
      } catch (error) {
        setLoading(false);
      }
    };

    fetchData();
  }, []);
  const hideDeleteProductDialog = () => {
    setDeleteProductDialog(false);
};
const deleteProduct = () => {
  
};
  const deleteProductDialogFooter = (
    <React.Fragment >
        <Button className="m-2" label="No" icon="pi pi-times" outlined onClick={hideDeleteProductDialog} />
        <Button className="m-2" label="Yes" icon="pi pi-check" severity="danger" onClick={deleteProduct} />
    </React.Fragment> 
);
 const actionBodyTemplate = (rowData) => {
  return (
      <React.Fragment>
          <Button icon="pi pi-trash" rounded outlined severity="danger" onClick={() => confirmDeleteProduct(rowData)} />
      </React.Fragment>
  );
};
const confirmDeleteProduct = (product) => {
  setProduct(product);
  setDeleteProductDialog(true);
};
  return (
    <div>
      {loading ? (
        <p>Loading products...</p>
      ) : (
        <div className="card mt-5">
          <Toolbar
            className="mb-5"
            left={leftToolbarTemplate}
            right={rightToolbarTemplate}
          ></Toolbar>
          <DataTable
            // ref={dt}
            value={products}
            selection={selectedProducts}
            onSelectionChange={(e) => setselectedProducts(e.value)}
            dataKey="id"
            paginator
            rows={10}
            rowsPerPageOptions={[5, 10, 25]}
            paginatorTemplate="FirstPageLink PrevPageLink PageLinks NextPageLink LastPageLink CurrentPageReport RowsPerPageDropdown"
            currentPageReportTemplate="Showing {first} to {last} of {totalRecords} products"
            // globalFilter={globalFilter}
            // header={header}
          >
            <Column selectionMode="multiple" exportable={false}></Column>
            <Column
              field="code"
              header="Code"
              sortable
              style={{ minWidth: "12rem" }}
            ></Column>
            <Column
              field="name"
              header="Name"
              sortable
              style={{ minWidth: "16rem" }}
            ></Column>
            <Column
              field="imageUrl"
              header="Image"
              body={imageBodyTemplate}
            ></Column>
            <Column
              field="price"
              header="Price"
              // body={priceBodyTemplate}
              sortable
              style={{ minWidth: "8rem" }}
            ></Column>
            <Column
              field="category"
              header="Category"
              sortable
              style={{ minWidth: "10rem" }}
            ></Column>
            <Column
              field="review"
              header="Reviews"
              body={ratingBodyTemplate}
              sortable
              style={{ minWidth: "12rem" }}
            ></Column>
            <Column
              field="inventoryStatus"
              header="Status"
              body={statusBodyTemplate}
              sortable
              style={{ minWidth: "12rem" }}
            ></Column>
            <Column
              body={actionBodyTemplate}
              exportable={false}
              style={{ minWidth: "12rem" }}
            ></Column>
          </DataTable>
          <Dialog visible={deleteProductDialog} style={{ width: '32rem' }} breakpoints={{ '960px': '75vw', '641px': '90vw' }} header="Confirm" modal footer={deleteProductDialogFooter} onHide={hideDeleteProductDialog}>
                <div className="confirmation-content">
                    <i className="pi pi-exclamation-triangle m-3" style={{ fontSize: '1.5rem' }} />
                    {product && (
                        <span>
                            Are you sure you want to delete <b>{product.name}</b>?
                        </span>
                    )}
                </div>
            </Dialog>
        </div>
      )}
    </div>
  );
};

export default ProductList;
