import { Button } from "primereact/button";
import { Rating } from "primereact/rating";
import { Tag } from "primereact/tag";
import React from "react";
const getSeverity = (product) => {
    let num = product.quantity;
    if(num>=15){
      return { color : "success" , value : 'INSTOCK'}
    }else if(num <=10 && num>0){
      return { color : "warning" , value : 'LOWSTOCK'}
    }else{
      return { color : "danger" , value : 'OUTOFSTOCK'}
    }
};
export const ratingBodyTemplate = (rowData) => {
    return <Rating value={rowData.review} readOnly cancel={false} />;
};
export const statusBodyTemplate = (rowData) => {
  return <Tag value={getSeverity(rowData).value} severity={getSeverity(rowData).color}></Tag>;
};
export const imageBodyTemplate = (rowData) => {
    return <img src={`${rowData.imageUrl}`} alt={rowData.image} className="shadow shadow-md border rounded-2" style={{ width: '64px' }} />;
};
