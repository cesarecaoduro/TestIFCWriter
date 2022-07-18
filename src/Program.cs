using GeometryGym.Ifc;

var db = new DatabaseIfc(ReleaseVersion.IFC4X3_RC4);



var facility = new IfcFacility(db, "My Facility")
{
    Description = "Just another facility",
};

var bridge = new IfcBridge(facility, null, null)
{
    Name = "My Bridge",
    Description = "Just another bridge"
};

var project = new IfcProject(facility, "My Project", IfcUnitAssignment.Length.Metre);
{
};


var foundations = new IfcFacilityPart(
    bridge,
    "Foundations",
    new IfcFacilityPartTypeSelect(IfcFacilityPartCommonTypeEnum.BELOWGROUND),
    IfcFacilityUsageEnum.NOTDEFINED
    );

var name = "Pile Cap";
var length = 1.2;
var width = 2.4;
var depth = 4.8;

IfcMaterial material = new IfcMaterial(db, "Concrete")
{
};

IfcFootingType footingType = new IfcFootingType(db, name, IfcFootingTypeEnum.PAD_FOOTING)
{
    MaterialSelect = material,
};

IfcRectangleProfileDef rect = new IfcRectangleProfileDef(db, "rect", length, width);
IfcExtrudedAreaSolid extrusion = new IfcExtrudedAreaSolid(rect, new IfcAxis2Placement3D(new IfcCartesianPoint(db, 0, 0, 0)), new IfcDirection(db, 0, 0, 1), depth);

IfcProductDefinitionShape productRep = new IfcProductDefinitionShape(new IfcShapeRepresentation(extrusion));
IfcShapeRepresentation shapeRep = new(extrusion);

footingType.RepresentationMaps.Add(
    new IfcRepresentationMap(
        db.Factory.XYPlanePlacement,
        shapeRep
    )
);

IfcFooting footing = new(
    foundations,
    null,
    productRep
    )
{
    PredefinedType = IfcFootingTypeEnum.PAD_FOOTING,
    ObjectType = name
};

db.WriteFile(Path.Combine("IFC4X3RC4_testBridge.ifc"));

//DatabaseIfc db = new DatabaseIfc(ModelView.Ifc4DesignTransfer);
//IfcBuilding building = new IfcBuilding(db, "IfcBuilding") { };
//IfcProject project = new IfcProject(building, "IfcProject", IfcUnitAssignment.Length.Millimetre) { };

////IfcBuildingStorey storey = new IfcBuildingStorey(building, "Ground Floor", 0);
//IfcMaterial masonryFinish = new IfcMaterial(db, "Masonry - Brick - Brown");
//IfcMaterial masonry = new IfcMaterial(db, "Masonry");
//IfcMaterialLayer layerFinish = new IfcMaterialLayer(masonryFinish, 110, "Finish");
//IfcMaterialLayer airInfiltrationBarrier = new IfcMaterialLayer(db, 50, "Air Infiltration Barrier") { IsVentilated = IfcLogicalEnum.TRUE };
//IfcMaterialLayer structure = new IfcMaterialLayer(masonry, 110, "Core");
//string name = "Double Brick - 270";
//IfcMaterialLayerSet materialLayerSet = new IfcMaterialLayerSet(new List<IfcMaterialLayer>() { layerFinish, airInfiltrationBarrier, structure }, name);
//IfcMaterialLayerSetUsage materialLayerSetUsage = new IfcMaterialLayerSetUsage(materialLayerSet, IfcLayerSetDirectionEnum.AXIS2, IfcDirectionSenseEnum.POSITIVE, 0);
//db.NextObjectRecord = 300;
//IfcWallType wallType = new IfcWallType(name, materialLayerSet, IfcWallTypeEnum.NOTDEFINED) { };
//IfcWallStandardCase wallStandardCase = new IfcWallStandardCase(building, materialLayerSetUsage, new IfcAxis2Placement3D(new IfcCartesianPoint(db, 0, 0, 0)), 5000, 2000) { };

//db.WriteFile(Path.Combine("IFC4_testWall.ifc"));
