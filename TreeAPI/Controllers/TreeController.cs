using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using TreeAPI.Models;
using TreeAPI.TreeData;

namespace TreeAPI.Controllers
{
    [ApiController]
    public class TreeController : ControllerBase
    {
        private ITreeData _treeData;
        public TreeController(ITreeData treeData)
        {
            _treeData = treeData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetRootNode()
        {
            TreeNode<NodeData> root = _treeData.GetRootNode();
            if (root != null)
            {
                return Ok(root.Data);
            }

            return Ok("Root not found");
        }

        [HttpGet]
        [Route("api/[controller]/id={id}")]
        public IActionResult GetNodeById(int id)
        {
            TreeNode<NodeData> node = _treeData.GetNodeById(id);
            if (node != null)
            {
                return Ok(node.Data);
            }

            return Ok("Node not found");
        }

        [HttpGet]
        [Route("api/[controller]/name={name}")]
        public IActionResult GetNodeByName(string name)
        {
            TreeNode<NodeData> node = _treeData.GetNodeByName(name);
            if (node != null)
            {
                return Ok(node.Data);
            }

            return Ok("Node not found");
        }

        [HttpPost]
        [Route("api/[controller]/add")]
        public IActionResult AddNode(AddNoteRequest request)
        {
            TreeNode<NodeData> newNode = _treeData.AddNode(request.NodeData, request.ParentId, request.Index);
            if (newNode != null)
            {
                return Ok("Added node successfully");
            }

            return Ok("Could not add node");
        }

        [HttpPost]
        [Route("api/[controller]/edit")]
        public IActionResult EditNode(NodeData nodeData)
        {
            TreeNode<NodeData> node = _treeData.EditNode(nodeData);
            if (node != null)
            {
                return Ok("Updated node successfully");
            }

            return Ok("Could not update node");
        }

        [HttpGet]
        [Route("api/[controller]/delete/{id}")]
        public IActionResult DeleteNode(int id)
        {
            
            if (_treeData.DeleteNode(id))
            {
                return Ok("Deleted node successfully");
            }

            return Ok("Could not delete node");
        }
    }
}
