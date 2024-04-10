# rabbitmq-queue-tutorial

## Prefetching Messages in RabbitMQ

The concept of prefetching messages before acknowledging is related to how consumers receive messages from RabbitMQ queues.

## Non-Prefetching Mode

In this mode, the consumer receives messages one at a time. After processing a message and acknowledging it, RabbitMQ sends the next message to the consumer.

## Prefetching Mode

In this mode, the consumer specifies a prefetch count, which indicates how many messages it wants RabbitMQ to deliver in advance before acknowledging them. This allows the consumer to buffer multiple messages locally before processing them. Once the consumer starts processing a message, RabbitMQ continues delivering messages up to the specified prefetch count in advance. This can help improve performance and throughput by reducing network round-trips and latency.


## Step 3: Install RabbitMQ Client NuGet Package
```
dotnet add package RabbitMQ.Client
```


## References

- [RabbitMQ Documentation](https://www.rabbitmq.com/documentation.html) - Official documentation for RabbitMQ.
- [RabbitMQ Tutorials](https://www.rabbitmq.com/getstarted.html) - Step-by-step tutorials to help you get started with RabbitMQ.
