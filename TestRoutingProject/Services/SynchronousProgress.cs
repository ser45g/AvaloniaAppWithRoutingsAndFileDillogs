﻿using System;

namespace TestRoutingProject.Business;

public sealed class SynchronousProgress<T>(Action<T> callback) : IProgress<T>
{
    void IProgress<T>.Report(T data) => callback(data);
}
