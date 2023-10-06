import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent extends BaseComponent implements OnInit {

  accountName: any;
  selectedStatus!: any;
  statusFilter: any;
  isEdit: any = false;

  filterStatusOrder: any = [
    { status: 0, name: 'Chờ xác nhận' },
    { status: 1, name: 'Chờ lấy hàng' },
    { status: 2, name: 'Đang giao' },
    { status: 3, name: 'Hoàn thành' },
    { status: 4, name: 'Đã hủy' },
    { status: 5, name: 'Chờ thanh toán' },
  ]

  statusOrder: any = [
    { status: 1, name: 'Chờ lấy hàng' },
    { status: 2, name: 'Đang giao' },
    { status: 3, name: 'Hoàn thành' },
    { status: 4, name: 'Đã hủy' },
  ]

  ngOnInit(): void {
    this.getListProduct(null);
    this.getListAllProduct();
    this.getListOrder(this.getRequest());
  }

  getRequest() {
    return {
      order_code: this.order_code_search ?? '',
      full_name: this.customer_search ?? '',
      phone: this.phone_search ?? '',
      status: this.status_search ?? null,
      type_payment: this.payment_search ?? null,
      created_at: this.order_date_search ?? null,
      deleted_at: this.cancle_date_search ?? null
    }
  }
  checkStatus(order: any, selectedStatus: any) {
    this.modal.confirm({
      nzTitle: '<i>Bạn có muốn thay đổi trạng thái ?</i>',
      // nzContent: '<b>Some descriptions</b>',
      nzOnOk: () => {
        this.orderService.updateStatus(order.order_id, selectedStatus.status).subscribe(
          (res) => {
            if (res.status == 200) {
              this.toastr.success('Thành công !');
              this.getListOrder(this.getRequest());
              this.handleCancel();
            }
            else {
              this.toastr.warning('Thất bại !');
            }
          }
        )
      }
    });
  }

  showConfirm(id: any): void {
    this.modal.confirm({
      nzTitle: '<i>Bạn có muốn xóa bản ghi này?</i>',
      // nzContent: '<b>Some descriptions</b>',
      nzOnOk: () => {
        this.orderService.delete(id).subscribe(
          (res) => {
            if (res.status == 200) {
              this.toastr.success('Thành công !');
              this.getListOrder(this.getRequest());
            }
            else {
              this.toastr.warning('Thất bại !');
              this.getListOrder(this.getRequest());
            }
          }
        )
      }
    });
  }

  confirmStatus(order: any, status: any) {
    this.modal.confirm({
      nzTitle: '<i>Bạn có chắc muốn cập nhật trạng thái đơn hàng này?</i>',
      // nzContent: '<b>Some descriptions</b>',
      nzOnOk: () => {
        this.orderService.updateStatus(order.order_id, status.status).subscribe(
          (res) => {
            if (res.status == 200) {
              this.toastr.success('Thành công !');
              this.getListOrder(this.getRequest());
            }
            else {
              this.toastr.warning('Fail !');
              this.getListOrder(this.getRequest());
            }
          }
        )
      }
    });
  }

  showDetail(hd: any) {
    this.isDisplay = true;
    this.orderInfo = hd;
    this.listProductCart = JSON.parse(hd.order_item);
  }

  orderID: any;
  showEdit(hd: any) {
    this.colorInput = null;
    this.orderID = hd.order_id;
    this.isEdit = true;
    this.orderInfo = hd;
    this.listProductCart = JSON.parse(hd.order_item);
    this.listProductCart.forEach((x: any) => {
      x.amount_bought = x.amountCart;
    });
  }

  handleOk(): void {
    console.log('Button ok clicked!');
    this.isDisplay = false;
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isDisplay = false;
    this.isEdit = false;
  }

  sumCart() {
    this.total = 0;
    this.listProductCart.forEach((data: any) => {
      this.total += data.price * data.amountCart;
    });
  }

  updateItem() {
    if (!this.listProductCart) {
      this.toastr.warning('Bạn chưa chọn sản phẩm');
      return false;
    }
    this.sumCart();
    var req = {
      Order: {
        order_id: this.orderID,
        order_item: JSON.stringify(this.listProductCart),
      },
    }
    this.orderService.updateOrderItemOnline(req).subscribe(
      (res: any) => {
        if (res.status == 200) {
          this.toastr.success('Cập nhật thành công');
          this.getListOrder(this.getRequest());
          this.handleCancel();
        }
        else {
          this.toastr.warning(res.message);
        }
      }
    );
    return true;
  }

  filter() {
    var req = {
      status: this.selectedStatus,
    }
    this.productService.getOrderByFilter(req).subscribe(
      (res) => {
        this.listOrder = res.data;
      }
    );
  }

  search() {
    this.getListOrder(this.getRequest());
  }

  setDisplayEdit(order: any): boolean {
    this.selected_ID = order.order_id;
    if ((order.status == 3) || (order.status == 4)) {
      return false;
    }
    return true;
  }

  addToCart(): boolean {
    if (this.product_code) {
      this.productFilter = this.listAllProduct.filter((x: any) => x.product_code == this.product_code);
    }
    if (this.product_name) {
      this.productFilter = this.listAllProduct.filter((x: any) => x.product_name.toLowerCase().includes(this.product_name));
    }
    if (this.size_search) {
      this.productFilter = this.listAllProduct.filter((x: any) => x.size == this.size_search);
    }
    if (this.colorInput) {
      this.productFilter = this.listAllProduct.filter((x: any) => x.color == this.colorInput);
    }

    let listAttributeCartId = this.listProductCart.map((x: any) => x.product_attribue_id) ?? [];

    if (this.productFilter) {
      this.productFilter.forEach((p: any) => {
        if (!listAttributeCartId.includes(p.product_attribue_id)) {
          p.amountCart = 1;
          p.totalPayment = 0;
          p.totalPayment = (p.price * p.amountCart);
          this.listProductCart.push(p);
        }
        else {
          this.toastr.warning('Sản phẩm này đã được thêm');
        }
      })
      this.toastr.success('Thêm sản phẩm thành công');
    }
    else {
      this.toastr.warning('Không tìm thấy sản phẩm nào phù hợp');
    }

    return true;
  }

  deleteCart(data: any): void {
    this.modal.confirm({
      nzTitle: '<i>Bạn có chắc muốn xóa sản phẩm này?</i>',
      // nzContent: '<b>Some descriptions</b>',
      nzOnOk: () => {
        this.listProductCart = this.listProductCart.filter((x: any) => x.product_attribue_id != data.product_attribue_id);
      }
    });
  }
}
