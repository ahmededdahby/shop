import { Button } from "primereact/button";
import React from "react";

const leftToolbarTemplate = () => {
  return (
    <div className="d-flex flex-wrap gap-4">
      <Button label="New" icon="pi pi-plus" severity="success" />
      <Button label="Delete" icon="pi pi-trash" severity="danger" />
    </div>
  );
};

export default leftToolbarTemplate;
