# Benchmark Playground

A small BenchmarkDotNet playground for comparing common C# loop patterns.

## Requirements

- .NET 10 SDK

## Run

```bash
dotnet run -c Release
```

## Benchmarks

- `ForLoop`
- `ForeachLoop`
- `LinqSum`

BenchmarkDotNet writes generated reports and logs to
`BenchmarkDotNet.Artifacts/`; this directory is intentionally ignored by Git.

## Disassembly diagnostics

`DisassemblyDiagnoser` is supported for native .NET disassembly on Windows and
Linux. On macOS, remove or conditionally disable `[DisassemblyDiagnoser]` to
run the timing and memory benchmarks locally. Run the same benchmark on
Windows or Linux when native JIT assembly output is required.
