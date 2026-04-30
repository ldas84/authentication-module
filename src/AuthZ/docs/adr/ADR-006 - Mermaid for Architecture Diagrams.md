# ADR-006 — Use of Mermaid for Architecture Diagrams

- **Status:** Accepted  
- **Date:** 2026-04-24  

## Context

The project requires diagrams for architecture, components, classes, ERDs, and sequences.  
These diagrams must be:

- Versionable in Git  
- Easy to update  
- Renderable directly in GitHub  
- Consistent across modules  
- Text-based (no binary files)

## Decision

We adopt **Mermaid** as the standard diagramming language for all project diagrams.

Types supported:

- Flowcharts  
- Sequence diagrams  
- Class diagrams  
- ERDs  
- Component diagrams  
- State diagrams  

All diagrams must be stored under:

docs/diagrams/<ModuleName>/


## Consequences

### Positive
- Diagrams render natively in GitHub  
- Easy to maintain and version  
- No proprietary tools required  
- Consistent visual language across modules  

### Negative
- Mermaid has limitations for very complex diagrams  
- Some contributors may need to learn the syntax  
