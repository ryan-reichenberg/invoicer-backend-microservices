import amqp, { Channel, Connection, Message } from 'amqplib/callback_api';

class RabbitClient {

  private onConnect(err: any, conn: Connection) {
    if (err !== null) return bail(err, conn);
    process.once('SIGINT', () => { conn.close(); });

    const q = 'hello';

    function onConnectOpen(err: any, ch: Channel) {
      ch.assertQueue(q, { durable: false }, (err, ok) => {
        if (err !== null) return bail(err, conn);
        ch.consume(q, (msg: Message | null) => { // message callback
          console.log(" [x] Received '%s'", msg?.content.toString());
        }, { noAck: true }, (_consumeOk) => { // consume callback
          console.log(' [*] Waiting for messages. To exit press CTRL+C');
        });
      });
    }

    conn.createChannel(onConnectOpen);
  }

  public connect() {
    amqp.connect(this.onConnect);
  }
}
function bail(err: any, conn: Connection) {
  console.error(err);
  if (conn) conn.close(() => { process.exit(1); });
}
