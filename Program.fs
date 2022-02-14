open System
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Diagnosers


[<Measure>]
type Chicken


[<Struct>]
type Kar<'T, [<Measure>] 'Measure> =
    struct
        val Values: array<'T>
        new (v: array<'T>) = { Values = v }
        member this.Item
            with get (i: int<'Measure>) =
                this.Values[int i]

            and set (index: int<'Measure>) value =
                this.Values[int index] <- value

        member this.Length = LanguagePrimitives.Int32WithMeasure<'Measure> this.Values.Length

    end

[<HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.CacheMisses)>]
[<DisassemblyDiagnoser(printSource = true, exportHtml = true)>]
type Benchmarks () =

    let rng = Random 123
    let loopCount = 10_000_000
    let arraySize = 16
    let randomLookups =
        [|
            for _ = 1 to loopCount * arraySize do
                rng.Next (arraySize)
        |]

    let typedRandomLookups =
        randomLookups
        |> Array.map (fun x -> x * 1<Chicken>)

    let lookupArray =
        [| for _ = 1 to arraySize do rng.Next (0, 100) |]

    let structTypedLookupArray =
        lookupArray
        |> Kar<_, Chicken>


    [<Benchmark>]
    member _.ArrayRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < randomLookups.Length - 1 do
            let nextLookup = randomLookups[i]
            result <- lookupArray[nextLookup]
            i <- i + 1

        result


    [<Benchmark>]
    member _.KarRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < typedRandomLookups.Length - 1 do
            let nextLookup = typedRandomLookups[i]
            result <- structTypedLookupArray[nextLookup]
            i <- i + 1

        result


    [<Benchmark>]
    member _.KarOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0<Chicken>
            while i < structTypedLookupArray.Length - 1<Chicken> do
                result <- structTypedLookupArray[i]
                i <- i + 1<Chicken>

        result


    [<Benchmark>]
    member _.ArrayOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0
            while i < lookupArray.Length - 1 do
                result <- lookupArray[i]
                i <- i + 1

        result


[<EntryPoint>]
let main argv =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    1
