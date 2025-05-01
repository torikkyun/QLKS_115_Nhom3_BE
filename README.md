# QLKS_115_Nhom3

## Lưu ý

Nhớ cập nhật chuỗi kết nối đến Database trong file `appsettings.Development.json`

## Hướng dẫn deploy

Trên local:

```bash
docker build -t qlks_115_nhom3_be .
docker save -o qlks_115_nhom3_be.tar qlks_115_nhom3_be
```

Trên server:

```bash
docker load -i qlks_115_nhom3_be.tar
docker run -d -p 5000:5000 --name qlks_115_nhom3_be qlks_115_nhom3_be
```