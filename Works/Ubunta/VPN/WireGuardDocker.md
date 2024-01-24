# WireGuard web
Блокируют в РФЮ


docker pull ghcr.io/wg-easy/wg-easy
<!-- Запускаем VPN -->

docker run -d \
  --name=Wireguard \
  -e WG_HOST=5.231.220.71 \
  -e PASSWORD=VPN_in_Francfurt2024! \
  -v ~/.wg-easy:/etc/wireguard \
  -p 51820:51820/udp \
  -p 51821:51821/tcp \
  --cap-add=NET_ADMIN \
  --cap-add=SYS_MODULE \
  --sysctl="net.ipv4.conf.all.src_valid_mark=1" \
  --sysctl="net.ipv4.ip_forward=1" \
  --restart unless-stopped \
  ghcr.io/wg-easy/wg-easy

sudo iptables -A INPUT -p tcp -s 95.214.116.10 --dport 51821 -j ACCEPT
  Подключаемся по 5182