terraform {
  required_providers {
    vsphere = {
      source  = "local/hashicorp/vsphere"
      version = ">= 2.6.1"
    }
  }
  required_version = ">= 0.13"
}
provider "vsphere" {
  user           = "${var.vsphere_user}"
  password       = "${var.vsphere_password}"
  vsphere_server = "${var.vsphere_server}"
  allow_unverified_ssl = true
}
data "vsphere_datacenter" "dc" {
  name = "702"
}
data "vsphere_datastore" "datastore" {
  name          = "${var.vsphere_pool}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
data "vsphere_datastore" "iso_datastore" {
  name          = "${var.vsphere_iso_pool}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
data "vsphere_resource_pool" "pool" {
  name          = "${var.vsphere_directory}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}
data "vsphere_network" "network" {
  name          = "${var.vsphere_vlan}"
  datacenter_id = "${data.vsphere_datacenter.dc.id}"
}